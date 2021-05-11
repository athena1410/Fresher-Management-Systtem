using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class FormatTypeEntityTypeConfiguration : IEntityTypeConfiguration<FormatType>
    {
        public void Configure(EntityTypeBuilder<FormatType> builder)
        {
            builder.ToTable("FormatTypes", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.FormatTypeName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Remarks)
                .HasMaxLength(500);

            builder.Property(x => x.IsDeleted)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property(x => x.RowVersion)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();

            builder.HasMany(x => x.Classes)
                .WithOne(c => c.FormatType)
                .HasForeignKey(x => x.FormatId);
        }
    }
}
