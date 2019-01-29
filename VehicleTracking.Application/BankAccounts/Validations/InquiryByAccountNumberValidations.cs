using VehicleTracking.Application.BankAccounts.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.BankAccounts.Validations
{
    public class InquiryByAccountNumberValidations : AbstractValidator<InquiryByAccountNumberQuery>
    {
        public InquiryByAccountNumberValidations()
        {
            RuleFor(a => a.AccountNumber)
                .MaximumLength(30)
                .NotEmpty();
        }
    }
}
