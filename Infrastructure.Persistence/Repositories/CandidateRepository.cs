using Application.Core.Interfaces.Repositories;
using Application.Domain.Entities;
using Ardalis.Specification;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Repositories
{
    public class CandidateRepository : Repository<ApplicationContext, Candidate>, ICandidateRepository
    {
        private readonly ILogger<CandidateRepository> _logger;

        public CandidateRepository(ApplicationContext context, ILogger<CandidateRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public CandidateRepository(ApplicationContext context, ISpecificationEvaluator specificationEvaluator) : base(context, specificationEvaluator)
        {
        }
    }
}
