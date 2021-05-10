using System.ComponentModel.DataAnnotations;
using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class TraineeCandidateProfileEntityTypeConfiguration : IEntityTypeConfiguration<TraineeCandidateProfile>
    {
        public void Configure(EntityTypeBuilder<TraineeCandidateProfile> builder)
        {
            builder.ToTable("TraineeCandidateProfiles", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.CandidateId);
            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.FullName)
                .HasColumnName("FullName")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.DateOfBirth)
                .HasColumnName("DOB")
                .IsRequired();

            builder.Property(x => x.Gender)
                .IsRequired();


            builder.Property(x => x.Phone)
                .HasMaxLength(14)
                .IsRequired();

            builder.HasIndex(x => x.Phone)
                .IsUnique();

            builder.Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.IsDeleted)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property(x => x.RowVersion)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();

            builder.HasOne(x => x.Candidate)
                .WithOne(x => x.TraineeCandidateProfile)
                .HasForeignKey<TraineeCandidateProfile>(x => x.CandidateId);

            builder.HasOne(x => x.University)
                .WithMany(x => x.TraineeCandidateProfiles)
                .HasForeignKey(x => x.UniversityId);

            builder.HasOne(x => x.Faculty)
                .WithMany(x => x.TraineeCandidateProfiles)
                .HasForeignKey(x => x.FacultyId);
        }
    }
}
