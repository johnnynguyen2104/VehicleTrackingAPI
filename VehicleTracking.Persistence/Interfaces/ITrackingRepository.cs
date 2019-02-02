using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Domain.TrackingEntities;

namespace VehicleTracking.Persistence.Interfaces
{
    /// <summary>
    /// This interface is defined as a base repository for enities belong to Tracking db
    /// </summary>
    /// <typeparam name="Aggregate"></typeparam>
    public interface ITrackingRepository<Aggregate> : IBaseRepository<Aggregate>
        where Aggregate : AggregateTrackingRoot
    {
    }
}
