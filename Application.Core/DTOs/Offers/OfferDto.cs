using System;

namespace Application.Core.DTOs.Offers
{
    public class OfferDto
    {
        public int Id { get; set; }
        public float JobRank { get; set; }
        public int Technology { get; set; }
        public float ContractType { get; set; }
        public uint OfferSalary { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
