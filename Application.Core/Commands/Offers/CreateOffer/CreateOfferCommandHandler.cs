using Application.Core.Interfaces.Repositories;
using AutoMapper;
using Common.Guard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.Entities;

namespace Application.Core.Commands.Offers.CreateOffer
{
    public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand>
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;

        public CreateOfferCommandHandler(
            IOfferRepository offerRepository,
            IMapper mapper)
        {
            _offerRepository = Guard.NotNull(offerRepository, nameof(offerRepository));
            _mapper = Guard.NotNull(mapper, nameof(mapper));
        }

        public async Task<Unit> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = _mapper.Map<Offer>(request);
            await _offerRepository.AddAsync(offer);
            await _offerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
