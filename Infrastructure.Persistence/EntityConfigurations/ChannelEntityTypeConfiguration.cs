using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class ChannelEntityTypeConfiguration : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.ToTable("Channels", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ChannelName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasColumnName("Deleted")
                .HasDefaultValue(false);

            builder.Property(x => x.RowVersion)
                .HasColumnName("RowVersion")
                .IsConcurrencyToken()
                .IsRowVersion();
             
            builder.HasMany(x => x.Candidates)
                .WithOne(c => c.Channel)
                .HasForeignKey(c => c.ChannelId);
        }
    }
}
