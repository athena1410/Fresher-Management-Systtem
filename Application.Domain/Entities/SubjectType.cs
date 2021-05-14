using System;
using System.Collections.Generic;

namespace Application.Domain.Entities
{
    public class SubjectType : Entity, IAuditableEntity
    {
        public string SubjectTypeName { get; set; }
        public string Remarks { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
