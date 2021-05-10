using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class EntryTestEntityTypeConfiguration : IEntityTypeConfiguration<EntryTest>
    {
        public void Configure(EntityTypeBuilder<EntryTest> builder)
        {
            builder.ToTable("EntryTests", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.IsDeleted)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property(x => x.RowVersion)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();

            builder.HasOne<Candidate>(x => x.Candidate)
                .WithMany(x => x.EntryTests)
                .HasForeignKey(x => x.CandidateId);

        }
    }
}
