using System;
using Application.Core.DTOs.Account;

namespace Application.Core.Commands.Account.RefreshToken
{
    public class RefreshTokenCommand: Command<IdentityResponseDto>
    {
        public string Token { get; private init; }

        public static RefreshTokenCommand CreateFromInput(string token, string createdBy)
        {
            return new RefreshTokenCommand
            {
                Token = token,
                CreatedBy = createdBy,
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
