using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Domain.Entities
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Nullable<DateTime> UpdatedDateTime { get; set; }
    }
}
