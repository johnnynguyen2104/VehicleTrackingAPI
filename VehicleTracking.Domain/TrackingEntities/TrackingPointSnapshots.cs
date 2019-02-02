using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.Entities;

namespace VehicleTracking.Domain.TrackingEntities
{
    public class TrackingPointSnapshots : AggregateTrackingRoot
    {
        public Guid VehicleReferencedCode { get; set; }

        public Guid StartPointId { get; set; }

        public string Note { get; set; }

        public virtual ICollection<TrackingPoints> TrackingPoints { get; set; }
    }
}
