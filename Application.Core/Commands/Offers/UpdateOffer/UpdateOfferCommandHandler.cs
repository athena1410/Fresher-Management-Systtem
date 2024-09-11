using Application.Core.Interfaces.Repositories;
using AutoMapper;
using Common.Guard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.Exceptions;

namespace Application.Core.Commands.Offers.UpdateOffer
{
    public class UpdateOfferCommandHandler(
        IOfferRepository offerRepository,
        IMapper mapper) : IRequestHandler<UpdateOfferCommand>
    {
        private readonly IOfferRepository _offerRepository = Guard.NotNull(offerRepository, nameof(offerRepository));
        private readonly IMapper _mapper = Guard.NotNull(mapper, nameof(mapper));

        public async Task Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await _offerRepository.GetByIdAsync(request.OfferId, cancellationToken);

            if (offer == null || offer.IsDeleted)
            {
                throw new NotFoundException(nameof(offer));
            }

            _mapper.Map(request, offer);
            await _offerRepository.UpdateAsync(offer);
            await _offerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
