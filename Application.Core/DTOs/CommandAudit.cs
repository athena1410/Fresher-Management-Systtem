using System;

namespace Application.Core.DTOs
{
    public class CommandAudit
    {
        public long Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public string Payload { get; set; }
        public string Result { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public TimeSpan ExecutionTime { get; set; }
    }
}
