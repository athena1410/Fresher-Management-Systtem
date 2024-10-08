﻿using Application.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.LocationName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Acronym)
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
