using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Common
{
    public class ErrorDetails
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
