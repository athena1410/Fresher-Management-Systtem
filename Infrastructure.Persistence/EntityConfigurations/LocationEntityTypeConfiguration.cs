using System;
using Application.Domain.Entities;
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

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property<DateTime>("_locationName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("LocationName")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property<DateTime>("_acronym")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Acronym")
                .HasMaxLength(10);
        }
    }
}
