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
            builder.Property(o => o.Id)
                .UseHiLo("CandidateSequence", ApplicationContext.DEFAULT_SCHEMA);

        }
    }
}
