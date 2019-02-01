using VehicleTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.EventEntities;

namespace VehicleTracking.Persistence.Configurations
{
    class TrackingPointConfiguration : IEntityTypeConfiguration<TrackingPoints>
    {
        public void Configure(EntityTypeBuilder<TrackingPoints> builder)
        {

            builder.Property(a => a.Latitude)
                .HasMaxLength(20)
                .HasColumnType("varchar")
                .IsRequired();

            builder.Property(a => a.Longitude)
                .HasMaxLength(20)
                .HasColumnType("varchar")
                .IsRequired();
        }
    }
}
