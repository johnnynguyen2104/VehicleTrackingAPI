﻿using VehicleTracking.Application.BankAccounts.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.BankAccounts.Validations
{
    public class WithdrawCommandValidation : AbstractValidator<WithdrawCommand>
    {
        public WithdrawCommandValidation()
        {

            RuleFor(a => a.AccountNumber)
                .MaximumLength(30)
                .NotEmpty();

            RuleFor(a => a.Currency)
              .MaximumLength(5)
              .NotEmpty();

            RuleFor(a => a.Amount)
                .GreaterThan(0);
        }
    }
}
