using VehicleTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleTracking.Domain.TrackingEntities;

namespace VehicleTracking.Persistence.Configurations
{
    public class TrackingPointSnapchotConfiguration : IEntityTypeConfiguration<TrackingPointSnapshots>
    {
        public void Configure(EntityTypeBuilder<TrackingPointSnapshots> builder)
        {
            builder.HasIndex(a => a.StartPointId)
                .IsUnique();

            builder.HasMany(a => a.TrackingPoints)
                .WithOne(a => a.SnapshotPoint)
                .HasForeignKey(a => a.SnapshotId);

            builder.Property(a => a.StartPointId)
                .HasColumnType("varchar(50)");

            //should define explicit type of varchar 
            //because ef automatically use Nvarchar for string 
            builder.Property(a => a.Note)
                .HasColumnType("varchar(100)");

            builder.Property(a => a.VehicleReferencedCode)
                .HasColumnType("varchar(50)")
                .IsRequired();
        }
    }
}
