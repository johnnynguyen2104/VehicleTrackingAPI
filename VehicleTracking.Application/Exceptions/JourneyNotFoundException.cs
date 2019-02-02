using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Exceptions
{
    public class JourneyNotFoundException : Exception
    {
        public JourneyNotFoundException(string message)
            :base(message)
        {

        }
    }
}
