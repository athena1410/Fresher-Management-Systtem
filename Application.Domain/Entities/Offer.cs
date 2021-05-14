using System;
using System.Collections.Generic;

namespace Application.Domain.Entities
{
    public class Offer : Entity, IAuditableEntity
    {
        public float JobRank { get; set; }
        public int Technology { get; set; }
        public float ContractType { get; set; }
        public uint OfferSalary { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
