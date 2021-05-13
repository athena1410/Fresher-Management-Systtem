using MediatR;
using System;

namespace Application.Core.Commands.Account.RevokeToken
{
    public class RevokeTokenCommand : Command<Unit>
    {
        public string Token { get; private init; }

        public static RevokeTokenCommand CreateFromInput(string token, string createdBy)
        {
            return new RevokeTokenCommand
            {
                Token = token,
                CreatedBy = createdBy,
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
