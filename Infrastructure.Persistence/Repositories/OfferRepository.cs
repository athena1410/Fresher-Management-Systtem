using Application.Core.Interfaces.Repositories;
using Application.Domain.Entities;
using Ardalis.Specification;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class OfferRepository : Repository<ApplicationContext, Offer>, IOfferRepository
    {
        public OfferRepository(ApplicationContext context) : base(context)
        {
        }

        public OfferRepository(ApplicationContext context, ISpecificationEvaluator specificationEvaluator) : base(context, specificationEvaluator)
        {
        }
    }
}
