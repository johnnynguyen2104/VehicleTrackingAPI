
using System;

namespace VehicleTracking.Application.Exceptions
{
    public class VehicleNotFoundException: Exception
    {
        public VehicleNotFoundException(string mess) : base(mess)
        {

        }
    }
}
