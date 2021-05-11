using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class SubjectTypeEntityTypeConfiguration : IEntityTypeConfiguration<SubjectType>
    {
        public void Configure(EntityTypeBuilder<SubjectType> builder)
        {
            builder.ToTable("SubjectTypes", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.SubjectTypeName)
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
                .WithOne(c => c.SubjectType)
                .HasForeignKey(x => x.SubjectTypeId);
        }
    }
}
