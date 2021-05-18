using Application.Core.DTOs.Offers;
using Application.Core.Interfaces;
using AutoMapper;
using Common.Guard;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace Application.Core.Queries.Offers
{
    public class GetOffersQueryHandler :IRequestHandler<Query<List<OfferDto>>, List<OfferDto>>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetOffersQueryHandler(
            IApplicationContext context, 
            IMapper mapper)
        {
            _context = Guard.NotNull(context, nameof(context));
            _mapper = Guard.NotNull(mapper, nameof(mapper));
        }

        public async Task<List<OfferDto>> Handle(Query<List<OfferDto>> request, CancellationToken cancellationToken)
        {
            var query = _context.Offers.Where(x => !x.IsDeleted)
                .ProjectTo<OfferDto>(_mapper.ConfigurationProvider);
            return await query.ToListAsync(cancellationToken);
        }
    }
}
