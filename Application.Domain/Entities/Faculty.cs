using System.Collections.Generic;

namespace Application.Domain.Entities
{
    public class Faculty : Entity
    {
        public string FacultyName { get; set; }
        public string Acronym { get; set; }
        public string Remarks { get; set; }

        public virtual ICollection<TraineeCandidateProfile> TraineeCandidateProfiles { get; set; }
    }
}
