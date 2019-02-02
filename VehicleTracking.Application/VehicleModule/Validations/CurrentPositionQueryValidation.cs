using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Application.VehicleModule.Queries;

namespace VehicleTracking.Application.VehicleModule.Validations
{
    public class CurrentPositionQueryValidation : AbstractValidator<CurrentPositionQuery>
    {
        public CurrentPositionQueryValidation()
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
