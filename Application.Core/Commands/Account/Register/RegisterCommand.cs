using System;
using Application.Core.DTOs.Account;
using MediatR;

namespace Application.Core.Commands.Account.Register
{
    public class RegisterCommand : Command<Unit>, IRequest<Unit>
    {
        public string Email { get; private init; }
        public string UserName { get; private init; }
        public string Password { get; private init; }

        public static RegisterCommand CreateFromInput(RegisterDto request, string createdBy)
        {
            return new RegisterCommand
            {
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
                CreatedDate = DateTimeOffset.Now,
                CreatedBy = createdBy
            };
        }
    }
}
