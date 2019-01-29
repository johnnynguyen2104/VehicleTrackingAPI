using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Infrastructure.Exceptions
{
    public class InvalidCurrencyException : Exception
    {
        public InvalidCurrencyException(string mess) : base(mess)
        {

        }
    }
}
