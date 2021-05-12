using System;
using Application.Core.DTOs.Account;
using MediatR;

namespace Application.Core.Commands.Account.Register
{
    public class RegisterCommand : Command<Unit>, IRequest<Unit>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public static RegisterCommand CreateFromInput(RegisterRequestDto request)
        {
            return new RegisterCommand
            {
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
                CreatedDate = DateTimeOffset.Now
            };
        }
    }
}
