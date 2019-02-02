
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Exceptions
{
    public class WithdrawFailException : Exception
    {
        public WithdrawFailException(string message) 
            : base(message)
        {

        }
    }
}
