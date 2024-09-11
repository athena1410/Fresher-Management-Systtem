using Application.Core.Interfaces.Repositories;
using AutoMapper;
using Common.Guard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.Entities;

namespace Application.Core.Commands.Offers.CreateOffer
{
    public class CreateOfferCommandHandler(
        IOfferRepository offerRepository,
        IMapper mapper) : IRequestHandler<CreateOfferCommand>
    {
        private readonly IOfferRepository _offerRepository = Guard.NotNull(offerRepository, nameof(offerRepository));
        private readonly IMapper _mapper = Guard.NotNull(mapper, nameof(mapper));

        public async Task Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = _mapper.Map<Offer>(request);
            await _offerRepository.AddAsync(offer);
            await _offerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
