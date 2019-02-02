using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracking.Application.Exceptions;
using VehicleTracking.Application.VehicleModule.Models;
using VehicleTracking.Common;
using VehicleTracking.Domain.Entities;
using VehicleTracking.Domain.TrackingEntities;
using VehicleTracking.Persistence.Interfaces;

namespace VehicleTracking.Application.VehicleModule.Queries
{
    public class JourneyQueryHandler : IRequestHandler<JourneyQuery, JourneyDto>
    {
        private readonly ITrackingRepository<TrackingPoints> _trackingPointRepository;
        private readonly ITrackingRepository<TrackingPointSnapshots> _trackingSnapshotRepository;
        private readonly IVehicleTrackingRepository<Vehicle> _vehicleRepository;

        private readonly IDateTime _machineDateTime;

        public JourneyQueryHandler(ITrackingRepository<TrackingPoints> trackingPointRepository
            , ITrackingRepository<TrackingPointSnapshots> trackingSnapshotRepository
            , IVehicleTrackingRepository<Vehicle> vehicleRepository
            , IDateTime machineDateTime)
        {
            _trackingPointRepository = trackingPointRepository;
            _trackingSnapshotRepository = trackingSnapshotRepository;
            _vehicleRepository = vehicleRepository;
            _machineDateTime = machineDateTime;
        }

        public Task<JourneyDto> Handle(JourneyQuery request, CancellationToken cancellationToken)
        {
            //Checking actived Vehicle.
            if (!_vehicleRepository.IsAny(a => a.IsActive
                                            && a.Id == request.VehicleId
                                            && a.DeviceCode == request.DeviceCode
                                            && a.ActivatedCode == request.ActivatedCode))
            {
                throw new VehicleNotFoundException($"Vehicle not found with Id {request.VehicleId} or the Vehicle was deactived.");
            }

            //Get 
            var journey = _trackingSnapshotRepository.Read(a => (a.CreatedDate >= request.FromDateTime && a.CreatedDate <= request.ToDateTime)
                                                                    && a.VehicleReferencedCode == request.VehicleId)
                                                                 .Include(a => a.TrackingPoints)
                                                                 .SelectMany(a => a.TrackingPoints)
                                                                 .Select(a => new PositionDto()
                                                                 {
                                                                     Latitude = a.Latitude,
                                                                     Longitude = a.Longitude,
                                                                     PositionDateTime = a.CreatedDate
                                                                 }).ToList();
            if (journey == null)
            {
                throw new JourneyNotFoundException($"Journey not found for vehicleid {request?.VehicleId}");
            }


            var latestTrackingPoint = new JourneyDto()
            {
                Positions = journey,
                VehicleId = request.VehicleId
            };

            return Task.FromResult(latestTrackingPoint);
        }
    }
}
