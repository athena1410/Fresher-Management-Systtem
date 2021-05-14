using System;
using System.Collections.Generic;

namespace Application.Domain.Entities
{
    public class Class : Entity, IAuditableEntity
    {
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public int TrainerId { get; set; }
        public int ClassAdminId { get; set; }
        public int LocationId { get; set; }
        public string DetailLocation { get; set; }
        public int SubjectTypeId { get; set; }
        public int SubSubjectTypeId { get; set; }
        public int DeliveryTypeId { get; set; }
        public int FormatId { get; set; }
        public int BudgetId { get; set; }
        public int ScopeId { get; set; }
        public int PlanedTraineeNumber { get; set; }
        public float EstimateBudget { get; set; }
        public DateTimeOffset? ExpectedStartDate { get; set; }
        public DateTimeOffset? ExpectedEndDate { get; set; }
        public DateTimeOffset? ActualStartDate { get; set; }
        public DateTimeOffset? ActualEndDate { get; set; }


        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Location Location { get; set; }
        public virtual Budget Budget { get; set; }
        public virtual Trainer Trainer { get; set; }
        public virtual Scope Scope { get; set; }
        public virtual FormatType FormatType { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual SubjectType SubjectType { get; set; }
        public virtual SubSubjectType SubSubjectType { get; set; }
        public virtual SupplierPartner SupplierPartner { get; set; }
        public virtual ICollection<Trainee> Trainees { get; set; }
    }
}
