using System;

namespace Application.Domain.Entities
{
    public class Interview : Entity, IAuditableEntity
    {
        public int CandidateId { get; set; }
        public int InterviewerId { get; set; }
        public DateTime Date { get; set; }
        public int Result { get; set; }
        public string Comments { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }


        public virtual Candidate Candidate { get; set; }
    }
}
