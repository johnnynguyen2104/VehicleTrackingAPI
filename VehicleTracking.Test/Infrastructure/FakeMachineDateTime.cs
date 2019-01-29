using VehicleTracking.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Test.Infrastructure
{
    public class FakeMachineDateTime : IDateTime
    {
        public DateTime Now => new DateTime(2018, 12, 6, 22, 30, 0, 0);

        public DateTime UtcNow => throw new NotImplementedException();
    }
}
