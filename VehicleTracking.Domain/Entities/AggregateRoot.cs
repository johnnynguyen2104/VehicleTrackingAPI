using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Domain.Entities
{
    public abstract class AggregateRoot
    {
        public int Id { get; set; }
    }
}
