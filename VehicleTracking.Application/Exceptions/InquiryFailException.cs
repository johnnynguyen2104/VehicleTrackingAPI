using VehicleTracking.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.BankAccounts.Exceptions
{
    public class InquiryFailException: BankingException
    {
        public InquiryFailException(string mess) : base(mess)
        {

        }
    }
}
