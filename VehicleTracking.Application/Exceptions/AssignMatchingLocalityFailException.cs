using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Exceptions
{
    public class AssignMatchingLocalityFailException : Exception
    {
        public AssignMatchingLocalityFailException(string message)
            : base(message)
        {

        }
    }
}
