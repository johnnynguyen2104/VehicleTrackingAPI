using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.Entities;

namespace VehicleTracking.Domain.EventEntities
{
    public class TrackingPointSnapshots : AggregateRoot
    {
        public string VehicleReferencedCode { get; set; }

        public string StartPointId { get; set; }

        public string Note { get; set; }

        public virtual TrackingPoints StartPoint { get; set; }
    }
}
