
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Exceptions
{
    public class CreateVehicleFailException : Exception
    {
        public CreateVehicleFailException(string message) : base(message)
        {
        }
    }
}
