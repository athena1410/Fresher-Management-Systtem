using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Interfaces.Repositories;
using Application.Domain.Entities;

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
            var candidate = new Candidate
            {
                Name = "Toanmk"
            };
            await _candidateRepository.AddAsync(candidate);
            _logger.LogError(candidate.Id.ToString());
            await _candidateRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
