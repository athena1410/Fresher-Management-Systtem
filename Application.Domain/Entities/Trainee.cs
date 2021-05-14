﻿using System;

namespace Application.Domain.Entities
{
    public class Trainee : Entity, IAuditableEntity
    {
        public int ClassId { get; set; }
        public int StatusId { get; set; }
        public int StatusInClassId { get; set; }
        public string Remarks { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Class Class { get; set; }
    }
}
