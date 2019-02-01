using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.Entities;

namespace VehicleTracking.Domain.EventEntities
{
    public class TrackingPoints : AggregateRoot
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public TrackingPointSnapshots SnapshotPoint { get; set; }
    }
}
