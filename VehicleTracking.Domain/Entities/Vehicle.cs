using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Domain.Entities
{
    public class Vehicle : AggregateRoot
    {
        public string DeviceCode { get; set; }

        public string ActivatedCode { get; set; }

        public bool IsActive { get; set; }

        public string RegisteredName { get; set; }

        public string RegisteredPhone { get; set; }

        public string DeviceModel { get; set; }
    }
}
