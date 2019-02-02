﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Vehicle.Models
{
    public class PositionDto
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public DateTime PositionDateTime { get; set; }
    }
}
