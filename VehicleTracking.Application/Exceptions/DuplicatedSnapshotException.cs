
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Exceptions
{
    public class DuplicatedSnapshotException : Exception
    {
        public DuplicatedSnapshotException(string message) 
            : base(message)
        {

        }
    }
}
