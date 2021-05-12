using MediatR;

namespace Application.Core.Commands.Account.Login
{
    public class LoginCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
