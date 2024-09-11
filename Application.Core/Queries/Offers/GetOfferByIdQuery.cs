using Application.Core.DTOs.Offers;
using Application.Core.Interfaces;
using AutoMapper;
using Common.Guard;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace Application.Core.Queries.Offers
{
    public class GetOfferByIdQueryHandler(
        IApplicationContext context,
        IMapper mapper) : IRequestHandler<GetByIdQuery<int, OfferDto>, OfferDto>
    {
        private readonly IApplicationContext _context = Guard.NotNull(context, nameof(context));
        private readonly IMapper _mapper = Guard.NotNull(mapper, nameof(mapper));

        public async Task<OfferDto> Handle(GetByIdQuery<int, OfferDto> request, CancellationToken cancellationToken)
        {
            var query = _context.Offers.Where(x => x.Id == request.Id && !x.IsDeleted)
                .ProjectTo<OfferDto>(_mapper.ConfigurationProvider);
            return await query.SingleOrDefaultAsync(cancellationToken);
        }
    }
}
