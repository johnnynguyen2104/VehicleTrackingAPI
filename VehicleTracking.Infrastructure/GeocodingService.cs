using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Application.VehicleModule.Models;

namespace VehicleTracking.Infrastructure
{
    public class GeocodingService : IGeocodingService
    {
        private readonly IConfiguration _configuration;

        public GeocodingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetAddressByLatLong(string lat, string longi)
        {
            using (var client = new HttpClient())
            {
                // New code:
                HttpResponseMessage response = await client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{longi}&key={_configuration["Configurations:API_Key"]}");
                var result = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(await response.Content.ReadAsStringAsync());

                if (result.ContainsKey("results"))
                {
                    return result["results"][0].formatted_address;
                }
            }

            return null;
        }
    }
}
