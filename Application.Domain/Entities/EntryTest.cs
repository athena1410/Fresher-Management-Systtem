using System;

namespace Application.Domain.Entities
{
    public class EntryTest : Entity, IAuditableEntity
    {
        public int CandidateId { get; set; }
        public DateTimeOffset Date { get; set; }
        public int LanguageEvaluator { get; set; }
        public int LanguageResult { get; set; }
        public int TechnicalEvaluator { get; set; }
        public int TechnicalResult { get; set; }
        public string Comments { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }


        public virtual Candidate Candidate { get; set; }
    }
}
