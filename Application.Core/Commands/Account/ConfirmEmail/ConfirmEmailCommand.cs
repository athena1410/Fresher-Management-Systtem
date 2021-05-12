using Application.Core.Interfaces.CQRS;
using MediatR;

namespace Application.Core.Commands.Account.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<bool>, ICommand<bool>
    {
        public string UserName { get; set; }
        public string Code { get; set; }

        public static ConfirmEmailCommand CreateCommandFromInput(string userName, string code)
        {
            return new ConfirmEmailCommand
            {
                UserName = userName,
                Code = code
            };
        }
    }
}
