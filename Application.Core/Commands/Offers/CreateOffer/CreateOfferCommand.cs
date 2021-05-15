using MediatR;

namespace Application.Core.Commands.Offers.CreateOffer
{
    public class CreateOfferCommand : Command<Unit>
    {
        public float JobRank { get; private init; }
        public int Technology { get; private init; }
        public float ContractType { get; private init; }
        public uint OfferSalary { get; private init; }
    }
}
