using System;
using System.Collections.Generic;

namespace Application.Domain.Entities
{
    public class Candidate : Entity, IAuditableEntity
    {
        public DateTimeOffset? ApplicationDate { get; set; }
        public int OfferId { get; set; }
        public int ChannelId { get; set; }
        public string Status { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual TraineeCandidateProfile TraineeCandidateProfile { get; set; }
        public virtual Channel Channel { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual ICollection<EntryTest> EntryTests { get; set; }
    }
}
