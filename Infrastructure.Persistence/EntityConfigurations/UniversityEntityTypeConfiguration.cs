using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class UniversityEntityTypeConfiguration : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.ToTable("Universities", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.UniversityName)
                .HasColumnName("UniversityName")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Acronym)
                .HasColumnName("Acronym")
                .HasMaxLength(10);

            builder.Property(x => x.IsDeleted)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property(x => x.RowVersion)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();
        }
    }
}
