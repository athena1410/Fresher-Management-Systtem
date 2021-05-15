using Application.Core.DTOs.Account;
using System;

namespace Application.Core.Commands.Account.Login
{
    public class LoginCommand : Command<IdentityResponseDto>
    {
        public string UserName { get; private init; }
        public string Password { get; private init; }

        public static LoginCommand CreateFromInput(IdentityDto request, string createdBy)
        {
            return new LoginCommand
            {
                UserName = request.UserName,
                Password = request.Password,
                CreatedBy = createdBy,
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
