using System;
using MediatR;

namespace Application.Core.Commands.Account.ConfirmEmail
{
    public class ConfirmEmailCommand : Command<Unit>
    {
        public string UserName { get; private init; }
        public string Code { get; private init; }

        public static ConfirmEmailCommand CreateFromInput(string userName, string code, string createdBy)
        {
            return new ConfirmEmailCommand
            {
                UserName = userName,
                Code = code,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = createdBy
            };
        }
    }
}
