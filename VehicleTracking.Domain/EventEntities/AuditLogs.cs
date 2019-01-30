using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.Entities;

namespace VehicleTracking.Domain.EventEntities
{
    public class AuditLogs : AggregateRoot
    {
        public string EntitiesName { get; set; }

        public string UpdatedField { get; set; }

        public string UpdatedData { get; set; }

        public string OldData { get; set; }

        public AuditAction Action { get; set; }
    }
}
