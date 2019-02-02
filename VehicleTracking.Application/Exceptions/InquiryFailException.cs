
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.BankAccounts.Exceptions
{
    public class InquiryFailException: Exception
    {
        public InquiryFailException(string mess) : base(mess)
        {

        }
    }
}
