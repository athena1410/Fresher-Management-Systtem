using Application.Core.Extensions;
using Application.Domain.Entities;
using Application.Domain.Exceptions;
using Common.Guard;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.Account.RevokeToken
{
    public class RevokeTokenCommandHandler(
        UserManager<ApplicationUser> userManager)
        : IRequestHandler<RevokeTokenCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager = Guard.NotNull(userManager, nameof(userManager));

        public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.FindByRefreshTokenAsync(request.Token, x => x.RefreshTokens);
            if (user is null)
            {
                throw new NotFoundException($"Token {request.Token} did not match with any user.");
            }

            var activeRefreshToken = user.RefreshTokens
                .FirstOrDefault(x => x.Token == request.Token && x.IsActive);

            if (activeRefreshToken is null)
            {
                throw new DomainException($"Token {request.Token} not active.");
            }

            // Revoke current token
            activeRefreshToken.RevokedDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
