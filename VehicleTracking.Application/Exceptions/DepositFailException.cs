
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Exceptions
{
    public class DepositFailException : Exception
    {
        public DepositFailException(string message) : base(message)
        {
        }
    }
}
