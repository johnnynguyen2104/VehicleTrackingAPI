using VehicleTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.EventEntities;

namespace VehicleTracking.Persistence.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLogs>
    {
        public void Configure(EntityTypeBuilder<AuditLogs> builder)
        {
            var converter = new EnumToStringConverter<AuditAction>();

            builder
                .Property(e => e.Action)
                .HasConversion(converter)
                .IsRequired();

            builder
                .Property(e => e.EntitiesName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(a => a.UpdatedData)
                .HasColumnType("varchar(max)");

            builder.Property(a => a.EntityId)
              .HasColumnType("varchar(50)")
              .IsRequired();

            builder.Property(a => a.OldData)
                .HasColumnType("varchar(max)");
        }
    }
}
