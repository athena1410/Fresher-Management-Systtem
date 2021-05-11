﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Entities
{
    public class Trainee : Entity, IAuditableEntity
    {
        public int ClassId { get; set; }
        public int StatusId { get; set; }
        public int StatusInClassId { get; set; }
        public string Remarks { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Class Class { get; set; }
    }
}
