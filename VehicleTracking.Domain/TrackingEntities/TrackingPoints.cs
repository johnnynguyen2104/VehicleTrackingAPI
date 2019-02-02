using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.Entities;

namespace VehicleTracking.Domain.TrackingEntities
{
    public class TrackingPoints : AggregateTrackingRoot
    {
        public Guid SnapshotId { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public TrackingPointSnapshots SnapshotPoint { get; set; }
    }
}
