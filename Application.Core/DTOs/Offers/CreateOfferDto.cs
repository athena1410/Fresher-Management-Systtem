namespace Application.Core.DTOs.Offers
{
    public class CreateOfferDto
    {
        public float JobRank { get; set; }
        public int Technology { get; set; }
        public float ContractType { get; set; }
        public uint OfferSalary { get; set; }
    }
}
