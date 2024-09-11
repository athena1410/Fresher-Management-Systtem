using Application.Domain.Entities;
using Application.Domain.Exceptions;
using Common.Guard;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.Account.ConfirmEmail
{
    public class ConfirmEmailCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<ConfirmEmailCommandHandler> logger)
        : IRequestHandler<ConfirmEmailCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager = Guard.NotNull(userManager, nameof(userManager));
        private readonly ILogger<ConfirmEmailCommandHandler> _logger = Guard.NotNull(logger, nameof(logger));

        public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                throw new NotFoundException($"Can't get user with UserName is {request.UserName}");
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, request.Code);
            if (result.Succeeded)
            {
                return Unit.Value;
            }
            _logger.LogError($"An error occurred while processing confirm email with command {request}");
            throw new DomainException(result.ToString());
        }
    }
}
