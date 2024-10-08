﻿using System;
using Application.Core.DTOs.Account;
using Application.Core.Enums;
using Application.Core.Extensions;
using Application.Core.Interfaces.Services;
using Application.Domain.Entities;
using Common.Guard;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Events.Account.Login;

namespace Application.Core.Commands.Account.Login
{
    public class LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITokenClaimService tokenClaimService,
        IMediator mediator,
        ILogger<LoginCommandHandler> logger)
        : IRequestHandler<LoginCommand, IdentityResponseDto>
    {
        private readonly UserManager<ApplicationUser> _userManager = Guard.NotNull(userManager, nameof(userManager));
        private readonly ITokenClaimService _tokenClaimService = Guard.NotNull(tokenClaimService, nameof(tokenClaimService));
        private readonly IMediator _mediator = Guard.NotNull(mediator, nameof(mediator));
        private readonly ILogger<LoginCommandHandler> _logger = Guard.NotNull(logger, nameof(logger));

        public async Task<IdentityResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(request.UserName, x => x.RefreshTokens);
            if (user == null)
            {
                return IdentityResponseDto.NotExisted($"User with {request.UserName} not existed.");
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return IdentityResponseDto.EmailNotConfirmed($"You need to confirm your email.");
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                return IdentityResponseDto.LockedOut($"Account has been locked.");
            }

            if (await _userManager.CheckPasswordAsync(user, request.Password))
            {

                JwtSecurityToken jwtSecurityToken = await _tokenClaimService.GenerateTokenAsync(user);
                var response = new IdentityResponseDto
                {
                    Status = LoginStatus.Success,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                    Message = "Success."
                };

                Domain.Entities.RefreshToken activeRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.IsActive);
                if (activeRefreshToken is null)
                {
                    activeRefreshToken = _tokenClaimService.GenerateRefreshToken();
                    user.RefreshTokens.Add(activeRefreshToken);
                    await _userManager.UpdateAsync(user);
                }

                response.RefreshToken = activeRefreshToken.Token;
                response.RefreshTokenExpiration = activeRefreshToken.Expires;

                _logger.LogInformation($"User {user.UserName} login success.");

                await _mediator.Publish(new LoginSuccessEvent
                {
                    UserName = user.UserName,
                    LoginSuccessAt = DateTimeOffset.UtcNow
                }, cancellationToken);

                return response;
            }

            return IdentityResponseDto.Failure($"Incorrect Credentials for user {user.UserName}.");
        }
    }
}
