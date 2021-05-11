using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class TrainerProfileEntityTypeConfiguration : IEntityTypeConfiguration<TrainerProfile>
    {
        public void Configure(EntityTypeBuilder<TrainerProfile> builder)
        {
            builder.ToTable("TrainerProfiles", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Account)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.DateOfBirth)
                .HasColumnName("DOB")
                .IsRequired();

            builder.Property(x => x.Gender)
                .IsRequired();

            builder.Property(x => x.Unit)
                .HasMaxLength(50);

            builder.Property(x => x.Major)
                .HasMaxLength(250);

            builder.Property(x => x.Remark)
                .HasMaxLength(500);


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

            builder.HasOne(x => x.Trainer)
                .WithOne(x => x.TrainerProfile)
                .HasForeignKey<TrainerProfile>(x => x.TrainerId);
        }
    }
}
