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
        }
    }
}
