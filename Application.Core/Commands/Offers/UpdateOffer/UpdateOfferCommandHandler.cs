using Application.Core.Interfaces.Repositories;
using Application.Domain.Entities;
using AutoMapper;
using Common.Guard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.Exceptions;

namespace Application.Core.Commands.Offers.UpdateOffer
{
    public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand>
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;

        public UpdateOfferCommandHandler(
            IOfferRepository offerRepository,
            IMapper mapper)
        {
            _offerRepository = Guard.Null(offerRepository, nameof(offerRepository));
            _mapper = Guard.Null(mapper, nameof(mapper));
        }

        public async Task<Unit> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            Offer offer = await _offerRepository.GetByIdAsync(request.OfferId, cancellationToken);

            if (offer == null || offer.IsDeleted)
            {
                throw new NotFoundException(nameof(offer));
            }

            _mapper.Map(request, offer);
            await _offerRepository.UpdateAsync(offer);
            await _offerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
