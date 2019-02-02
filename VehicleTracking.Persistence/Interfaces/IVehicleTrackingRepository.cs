using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.Entities;

namespace VehicleTracking.Persistence.Interfaces
{
    /// <summary>
    /// This interface is defined as a base repository for enities belong to Vehicle db
    /// </summary>
    /// <typeparam name="Aggregate"></typeparam>
    public interface IVehicleTrackingRepository<Aggregate> : IBaseRepository<Aggregate>
        where Aggregate : AggregateRoot
    {
    }
}
