using VehicleTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.TrackingEntities;

namespace VehicleTracking.Persistence.Configurations
{
    class TrackingPointConfiguration : IEntityTypeConfiguration<TrackingPoints>
    {
        public void Configure(EntityTypeBuilder<TrackingPoints> builder)
        {
            builder.Property(a => a.SnapshotId)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.Property(a => a.Latitude)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(a => a.Longitude)
                .HasColumnType("varchar(20)")
                .IsRequired();
        }
    }
}
