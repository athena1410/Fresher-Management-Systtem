using System;

namespace Application.Domain.Entities
{
    public class Class :Entity, IAuditableEntity
    {

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
