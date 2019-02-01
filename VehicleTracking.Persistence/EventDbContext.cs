using Microsoft.EntityFrameworkCore;
using VehicleTracking.Persistence.Configurations;

namespace VehicleTracking.Persistence
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<VehicleTrackingDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TrackingPointConfiguration());
            modelBuilder.ApplyConfiguration(new TrackingPointSnapchotConfiguration());
        }
    }
}
