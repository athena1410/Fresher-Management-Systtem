using MediatR;

namespace Application.Core.Commands.User.CreateUser
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
