using System.Collections.Generic;

namespace Application.Domain.Entities
{
    public class Channel : Entity
    {
        public int? EmployeeId { get; set; }
        public string ChannelName { get; set; }
        public string Remarks { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
