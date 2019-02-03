using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Application.VehicleModule.Models;

namespace VehicleTracking.Application.Interfaces
{
    public interface IGeocodingService
    {
        Task<string> GetAddressByLatLong(string lat, string longi);
    }
}
