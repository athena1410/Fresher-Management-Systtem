using System;
using System.Collections.Generic;

namespace Application.Domain.Entities
{
    public class Offer : Entity, IAuditableEntity
    {
        public int JobRank { get; set; }
        public int Technology { get; set; }
        public int ContractType { get; set; }
        public int OfferSalary { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
