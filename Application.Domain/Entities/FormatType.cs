﻿using System;
using System.Collections.Generic;

namespace Application.Domain.Entities
{
    public class FormatType : Entity, IAuditableEntity
    {
        public string FormatTypeName { get; set; }
        public string Remarks { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
