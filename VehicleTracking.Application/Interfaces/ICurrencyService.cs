﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<decimal> GetRates(string baseCurrency, string exchangeCurrency);
    }
}
