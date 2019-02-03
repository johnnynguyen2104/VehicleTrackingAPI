using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Exceptions
{
    public class LatestSnapshotNotFoundException : Exception
    {
        public LatestSnapshotNotFoundException(string message)
            :base(message)
        {

        }
    }
}
