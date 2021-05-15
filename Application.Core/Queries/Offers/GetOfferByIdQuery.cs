using Application.Core.DTOs.Offers;
using Application.Core.Interfaces;
using AutoMapper;
using Common.Guard;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Queries.Offers
{
    public class GetOfferByIdQueryHandler : IRequestHandler<GetByIdQuery<int, OfferDto>, OfferDto>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetOfferByIdQueryHandler(
            IApplicationContext context,
            IMapper mapper)
        {
            _context = Guard.Null(context, nameof(context));
            _mapper = Guard.Null(mapper, nameof(mapper));
        }

        public async Task<OfferDto> Handle(GetByIdQuery<int, OfferDto> request, CancellationToken cancellationToken)
        {
            var query = _context.Offers.Where(x => x.Id == request.Id && !x.IsDeleted);
            return await _mapper.ProjectTo<OfferDto>(query).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
