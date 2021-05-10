namespace Application.Domain.Entities
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
