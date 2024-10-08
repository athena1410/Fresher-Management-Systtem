﻿using Application.Core.DTOs.Emails;
using Application.Core.Interfaces.Services;
using Application.Domain.Entities;
using Application.Domain.Exceptions;
using Common.Guard;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Events.Account.Register;
using Microsoft.AspNetCore.WebUtilities;

namespace Application.Core.Commands.Account.Register
{
    public class RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        IMediator mediator,
        ILogger<RegisterCommandHandler> logger)
        : IRequestHandler<RegisterCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager = Guard.NotNull(userManager, nameof(userManager));
        private readonly ILogger<RegisterCommandHandler> _logger = Guard.NotNull(logger, nameof(logger));
        private readonly IEmailService _emailService = Guard.NotNull(emailService, nameof(emailService));
        private readonly IMediator _mediator = Guard.NotNull(mediator, nameof(mediator));

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser userWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithEmail != null)
            {
                throw new DuplicateException($"User with email {request.Email} is already existed.");
            }

            ApplicationUser userWithUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithUserName != null)
            {
                throw new DuplicateException($"User with request.UserName {request.UserName} is already existed.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) throw new DomainException(result.ToString());

            await _userManager.AddToRoleAsync(user, Enums.Role.User.ToString());
            string confirmCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            _logger.LogInformation($"Generated confirm code : {confirmCode} with user {request.UserName}");

            await _emailService.SendAsync(BuildConfirmEmail(user, confirmCode));

            await _mediator.Publish(new RegisteredEvent
            {
                Email = user.Email,
                UserName = user.UserName
            }, cancellationToken);
        }

        private EmailMessageDto BuildConfirmEmail(ApplicationUser user, string code)
        {
            var confirmLink = BuildConfirmLink(user, code);

            var mailMessage = new EmailMessageDto()
            {
                From = "duytoan.mk@gmail.com",
                Body = $"Please confirm your account by visiting this URL {confirmLink}",
                Subject = "Confirm Registration",
                To = user.Email
            };
            return mailMessage;
        }

        private string BuildConfirmLink(ApplicationUser user, string code)
        {
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            const string route = "api/v1/account/confirm-email/";
            var verificationUri = QueryHelpers.AddQueryString(route, "userName", user.UserName);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            return verificationUri;
        }
    }
}
