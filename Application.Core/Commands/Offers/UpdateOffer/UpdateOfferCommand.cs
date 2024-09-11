using MediatR;

namespace Application.Core.Commands.Offers.UpdateOffer
{
    public class UpdateOfferCommand : Command
    {
        public int OfferId { get; set; }
        public float JobRank { get; private init; }
        public int Technology { get; private init; }
        public float ContractType { get; private init; }
        public uint OfferSalary { get; private init; }
    }
}
