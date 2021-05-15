using System;

namespace Application.Core.DTOs.Candidates
{
    public class CandidateDto
    {
        public int Id { get; set; }
        public DateTimeOffset? ApplicationDate { get; set; }
        public int OfferId { get; set; }
        public int ChannelId { get; set; }
        public string Status { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
