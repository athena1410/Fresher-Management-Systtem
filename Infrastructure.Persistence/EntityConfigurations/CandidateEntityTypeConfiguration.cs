using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class CandidateEntityTypeConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("Candidates", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.IsDeleted)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property(x => x.RowVersion)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();

            builder.HasMany(x => x.Interviews)
                .WithOne(x => x.Candidate)
                .HasForeignKey(x => x.CandidateId);

            builder.HasOne(x => x.Offer)
                .WithMany(x => x.Candidates)
                .HasForeignKey(x => x.OfferId);

            //builder.Property(o => o.Id)
            //    .UseHiLo("CandidateSequence", ApplicationContext.DEFAULT_SCHEMA);

        }
    }
}
