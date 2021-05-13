using Application.Core.DTOs.Account;
using MediatR;
using System;

namespace Application.Core.Commands.Account.Login
{
    public class LoginCommand : Command<LoginResponseDto>, IRequest<LoginResponseDto>
    {
        public string UserName { get; private init; }
        public string Password { get; private init; }

        public static LoginCommand CreateFromInput(LoginRequestDto request, string createdBy)
        {
            return new LoginCommand
            {
                UserName = request.UserName,
                Password = request.Password,
                CreatedBy = createdBy,
                CreatedDate = DateTimeOffset.Now
            };
        }
    }
}
