using VehicleTracking.Application.Interfaces;
using VehicleTracking.Infrastructure.Exceptions;
using VehicleTracking.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Infrastructure
{
    public class CurrencyService : ICurrencyService
    {
        public Task<decimal> GetRates(string baseCurrency, string exchangeCurrency)
        {
            return Task.Run(() =>
            {

                var mockData = new CurrencyInfo()
                {
                    BaseCurrency = "THB",
                    Date = DateTime.Now,
                    Rates = new Dictionary<string, decimal>()
                {
                    { "USD", 33 },
                    { "EUR", 40 }

                }
                };

                if (!mockData.Rates.ContainsKey(exchangeCurrency))
                {
                    return 0;
                }
                return mockData.Rates[exchangeCurrency];
            });
        }
    }
}
