using Application.Core.Interfaces.Services;
using Common.Guard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.Account.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IAuthenticationService _authenticationService;

        public ConfirmEmailCommandHandler(
            IAuthenticationService authenticationService)
        {
            this._authenticationService = Guard.Null(authenticationService, nameof(authenticationService));
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.ConfirmEmailAsync(request.UserName, request.Code);
        }
    }
}
