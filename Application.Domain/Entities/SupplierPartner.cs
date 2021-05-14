using System;

namespace Application.Domain.Entities
{
    public class SupplierPartner : Entity, IAuditableEntity
    {
        public int ClassId { get; set; }
        public string SupplierPartnerName { get; set; }
        public string Remarks { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Class Class { get; set; }
    }
}
