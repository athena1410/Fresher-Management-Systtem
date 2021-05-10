using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class OfferEntityTypeConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offers", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(o => o.ContractType)
                .IsRequired();

            builder.Property(o => o.JobRank)
                .IsRequired();

            builder.Property(o => o.OfferSalary)
                .IsRequired();

            builder.Property(o => o.Technology)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property(x => x.RowVersion)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();

            builder.HasMany(x => x.Candidates)
                .WithOne(c => c.Offer)
                .HasForeignKey(c => c.OfferId);


        }
    }
}
