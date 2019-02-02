using Microsoft.EntityFrameworkCore;
using VehicleTracking.Persistence.Configurations;

namespace VehicleTracking.Persistence
{
    public class TrackingDbContext : DbContext
    {
        public TrackingDbContext(DbContextOptions<TrackingDbContext> options)
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
