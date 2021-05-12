using Application.Core.Interfaces.Services;
using Common.Guard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.Account.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterCommandHandler(
            IAuthenticationService authenticationService)
        {
            this._authenticationService = Guard.Null(authenticationService, nameof(authenticationService));
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.RegisterAsync(request.UserName, request.Email, request.Password);
            return Unit.Value;;
        }
    }
}
