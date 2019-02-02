using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Application.Vehicle.Queries;

namespace VehicleTracking.Application.Vehicle.Validations
{
    public class JourneyQueryValidation : AbstractValidator<JourneyQuery>
    {
        public JourneyQueryValidation()
        {

        }
    }
}
