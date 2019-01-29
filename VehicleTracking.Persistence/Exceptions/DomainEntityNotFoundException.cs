using VehicleTracking.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Persistence.Exceptions
{
    public class DomainEntityNotFoundException : BankingException
    {
        public DomainEntityNotFoundException(string message):base(message)
        {
        }
    }
}
