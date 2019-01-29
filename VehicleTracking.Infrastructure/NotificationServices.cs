using VehicleTracking.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace VehicleTracking.Infrastructure
{
    public class NotificationServices : INotificationServices
    {
        public Task SendNotification()
        {
            return Task.CompletedTask;
        }
    }
}
