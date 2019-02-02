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

        }
    }
}
