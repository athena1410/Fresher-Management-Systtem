using Application.Core.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly ICandidateRepository _candidateRepository;
        public CreateUserCommandHandler(
            ICandidateRepository candidateRepository,
            ILogger<CreateUserCommandHandler> logger)
        {
            this._candidateRepository = candidateRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }
    }
}
