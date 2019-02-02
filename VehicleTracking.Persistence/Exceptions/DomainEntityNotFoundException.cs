
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Persistence.Exceptions
{
    public class DomainEntityNotFoundException : Exception
    {
        public DomainEntityNotFoundException(string message):base(message)
        {
        }
    }
}
