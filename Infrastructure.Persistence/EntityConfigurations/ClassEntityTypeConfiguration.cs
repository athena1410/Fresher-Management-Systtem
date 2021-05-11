using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class ClassEntityTypeConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Classes", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ClassName)
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(x => x.ClassCode)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(x => x.Location)
                .WithMany(x => x.Classes)
                .HasForeignKey(x => x.LocationId);

            builder.Property(x => x.IsDeleted)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property(x => x.RowVersion)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();

            builder.HasOne(x => x.DeliveryType)
                .WithMany(x => x.Classes)
                .HasForeignKey(x => x.DeliveryTypeId);

            builder.HasOne(x => x.FormatType)
                .WithMany(x => x.Classes)
                .HasForeignKey(x => x.FormatId);

            builder.HasOne(x => x.SupplierPartner)
                .WithOne(x => x.Class)
                .HasForeignKey<SupplierPartner>(x => x.ClassId);

            builder.HasMany(x => x.Trainees)
                .WithOne(x => x.Class)
                .HasForeignKey(x => x.ClassId);
        }
    }
}
