using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.Account.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }
    }
}
