using VehicleTracking.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
