using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Security.Models;

namespace VehicleTracking.Security.Validations
{
    public class RegisterValidation : AbstractValidator<RegisterModel>
    {
        public RegisterValidation()
        {
            RuleFor(a => a.Email)
                .NotNull()
                .NotEmpty();

            RuleFor(a => a.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
