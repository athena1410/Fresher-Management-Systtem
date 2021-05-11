using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class SupplierPartnerEntityTypeConfiguration : IEntityTypeConfiguration<SupplierPartner>
    {
        public void Configure(EntityTypeBuilder<SupplierPartner> builder)
        {
            builder.ToTable("SupplierPartners", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.SupplierPartnerName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property(x => x.RowVersion)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();

            builder.HasOne(x => x.Class)
                .WithOne(c => c.SupplierPartner)
                .HasForeignKey<SupplierPartner>(x => x.ClassId);
        }
    }
}
