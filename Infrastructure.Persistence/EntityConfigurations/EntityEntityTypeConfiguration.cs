using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class EntityEntityTypeConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property<byte[]>("_rowVersion")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();
        }
    }
}
