using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Domain.Entities
{
    public class TrainerProfile: Entity, IAuditableEntity
    {
        public int TrainerId { get; set; }
        public string FullName { get; set; }
        public string Account { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gender { get; set; }
        public string Unit { get; set; }
        public string Major { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Experience { get; set; }
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Trainer Trainer { get; set; }
    }
}
