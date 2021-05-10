using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class InterviewEntityTypeConfiguration : IEntityTypeConfiguration<Interview>
    {
        public void Configure(EntityTypeBuilder<Interview> builder)
        {
            builder.ToTable("Interviews", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(ci => new {ci.CandidateId, ci.InterviewerId});
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.IsDeleted)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property(x => x.RowVersion)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();

            builder.HasOne(x => x.Candidate)
                .WithMany(x => x.Interviews)
                .HasForeignKey(x => x.CandidateId);

        }
    }
}
