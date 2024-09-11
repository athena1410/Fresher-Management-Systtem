using MediatR;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.DTOs.Account;
using Application.Core.Enums;
using Application.Core.Extensions;
using Application.Core.Interfaces.Services;
using Application.Domain.Entities;
using Common.Guard;
using Microsoft.AspNetCore.Identity;

namespace Application.Core.Commands.Account.RefreshToken
{
    public class RefreshTokenCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITokenClaimService tokenClaimService)
        : IRequestHandler<RefreshTokenCommand, IdentityResponseDto>
    {
        private readonly UserManager<ApplicationUser> _userManager = Guard.NotNull(userManager, nameof(userManager));
        private readonly ITokenClaimService _tokenClaimService = Guard.NotNull(tokenClaimService, nameof(tokenClaimService));

        public async Task<IdentityResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.FindByRefreshTokenAsync(request.Token, x => x.RefreshTokens);
            if (user is null)
            {
                return IdentityResponseDto.NotExisted($"Token {request.Token} did not match with nay user.");
            }

            var activeRefreshToken = user.RefreshTokens
                .FirstOrDefault(x => x.Token == request.Token && x.IsActive);

            if (activeRefreshToken is null)
            {
                return IdentityResponseDto.Failure($"Token {request.Token} not active.");
            }

            // Revoke current token
            activeRefreshToken.RevokedDate = DateTime.UtcNow;
            // Generate new token and refresh token
            Domain.Entities.RefreshToken newRefreshToken = _tokenClaimService.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            JwtSecurityToken jwtSecurityToken = await _tokenClaimService.GenerateTokenAsync(user);
            return new IdentityResponseDto
            {
                Status = LoginStatus.Success,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
                Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                Message = "Success.",
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.Expires
            };
        }
    }
}
