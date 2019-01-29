using VehicleTracking.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Exceptions
{
    public class DepositFailException : BankingException
    {
        public DepositFailException(string message) : base(message)
        {
        }
    }
}
