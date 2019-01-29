using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Application.Interfaces
{
    public interface INotificationServices
    {
        Task SendNotification();
    }
}
