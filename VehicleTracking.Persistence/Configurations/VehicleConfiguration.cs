using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.Entities;

namespace VehicleTracking.Persistence.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.Property(a => a.ActivatedCode)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(a => a.ActivatedCode)
                .IsUnique();

            builder.Property(a => a.DeviceCode)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(a => a.DeviceCode)
                .IsUnique();

            builder.Property(a => a.DeviceModel)
                 .HasColumnType("varchar")
                 .HasMaxLength(50)
                 .IsRequired();

            builder.Property(a => a.IsActive)
                  .IsRequired();

            builder.Property(a => a.RegisteredName)
                .IsRequired();

            builder.Property(a => a.RegisteredPhone)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(a => a.RegisteredPhone)
               .IsUnique();
        }
    }
}
