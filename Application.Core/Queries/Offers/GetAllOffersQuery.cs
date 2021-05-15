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
            _context = Guard.Null(context, nameof(context));
            _mapper = Guard.Null(mapper, nameof(mapper));
        }

        public async Task<List<OfferDto>> Handle(Query<List<OfferDto>> request, CancellationToken cancellationToken)
        {
            var query = _context.Offers.Where(x => !x.IsDeleted);
            return await _mapper.ProjectTo<OfferDto>(query).ToListAsync(cancellationToken);
        }
    }
}
