using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Application.VehicleModule.Commands;

namespace VehicleTracking.Application.VehicleModule.Validations
{
    public class RegisterVehicleCommandValidation: AbstractValidator<RegisterVehicleCommand>
    {
        public RegisterVehicleCommandValidation()
        {
            RuleFor(a => a.DeviceModel)
               .NotNull()
               .NotEmpty();

            RuleFor(a => a.DeviceCode)
                .NotNull()
                .NotEmpty();

            RuleFor(a => a.ActivatedCode)
                .NotNull()
                .NotEmpty();

            RuleFor(a => a.RegisteredName)
             .NotNull()
             .NotEmpty();

            RuleFor(a => a.RegisteredPhone)
             .NotNull()
             .NotEmpty();
        }
    }
}
