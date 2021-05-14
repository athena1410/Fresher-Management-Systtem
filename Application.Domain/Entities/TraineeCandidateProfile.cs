using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Domain.Entities
{
    public class TraineeCandidateProfile : Entity, IAuditableEntity
    {
        public int CandidateId { get; set; }
        public int UniversityId { get; set; }
        public int FacultyId { get; set; }
        public string FullName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public short Gender { get; set; }
        public DateTimeOffset? GraduationDate { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Type { get; set; }
        public int? Skill { get; set; }
        public string ForeignLanguage { get; set; }
        public int? Level { get; set; }
        public string AllocationStatus { get; set; }
        public string History { get; set; }
        public string Remarks { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual University University { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
