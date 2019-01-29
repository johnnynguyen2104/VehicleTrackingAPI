using VehicleTracking.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Exceptions
{
    public class WithdrawFailException : BankingException
    {
        public WithdrawFailException(string message) 
            : base(message)
        {

        }
    }
}
