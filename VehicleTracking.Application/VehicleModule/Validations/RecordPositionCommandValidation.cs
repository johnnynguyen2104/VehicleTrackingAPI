using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Application.VehicleModule.Commands;

namespace VehicleTracking.Application.VehicleModule.Validations
{
    public class RecordPositionCommandValidation : AbstractValidator<RecordPositionCommand>
    {
        public RecordPositionCommandValidation()
        {
            RuleFor(a => a.VehicleId)
               .NotNull()
               .NotEmpty();

            RuleFor(a => a.DeviceCode)
                .NotNull()
                .NotEmpty();

            RuleFor(a => a.ActivatedCode)
                .NotNull()
                .NotEmpty();

            RuleFor(a => a.Latitude)
               .NotNull()
               .NotEmpty()
               .Matches(@"^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$")
               .WithMessage("Invalid Latitude.");

            RuleFor(a => a.Longitude)
              .NotNull()
              .NotEmpty()
              .Matches(@"^(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$")
              .WithMessage("Invalid Longitude."); ;
        }
    }
}
