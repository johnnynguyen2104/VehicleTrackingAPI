using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracking.Application.Exceptions;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Application.VehicleModule.Models;
using VehicleTracking.Common;
using VehicleTracking.Domain.Entities;
using VehicleTracking.Domain.TrackingEntities;
using VehicleTracking.Persistence.Interfaces;

namespace VehicleTracking.Application.VehicleModule.Queries
{
    public class CurrentPositionQueryHandler : IRequestHandler<CurrentPositionQuery, CurrentPositionDto>
    {
        private readonly ITrackingRepository<TrackingPoints> _trackingPointRepository;
        private readonly ITrackingRepository<TrackingPointSnapshots> _trackingSnapshotRepository;
        private readonly IVehicleTrackingRepository<Vehicle> _vehicleRepository;
        private readonly IGeocodingService _geocodingService;
        private readonly IDateTime _machineDateTime;

        public CurrentPositionQueryHandler(ITrackingRepository<TrackingPoints> trackingPointRepository
            , ITrackingRepository<TrackingPointSnapshots> trackingSnapshotRepository
            , IVehicleTrackingRepository<Vehicle> vehicleRepository
            , IDateTime machineDateTime
            , IGeocodingService geocodingService)
        {
            _trackingPointRepository = trackingPointRepository;
            _trackingSnapshotRepository = trackingSnapshotRepository;
            _vehicleRepository = vehicleRepository;
            _machineDateTime = machineDateTime;
            _geocodingService = geocodingService;
        }

        public async Task<CurrentPositionDto> Handle(CurrentPositionQuery request, CancellationToken cancellationToken)
        {
            //Checking actived Vehicle.
            if (!_vehicleRepository.IsAny(a => a.IsActive
                                            && a.Id == request.VehicleId
                                            && a.DeviceCode == request.DeviceCode
                                            && a.ActivatedCode == request.ActivatedCode))
            {
                throw new VehicleNotFoundException($"Vehicle not found with Id {request.VehicleId} or the Vehicle was deactived.");
            }

            
            var latestTrackingPoint = _trackingSnapshotRepository.Read(a => a.CreatedDate >= _machineDateTime.UtcNow.Date
                                                                    && a.VehicleReferencedCode == request.VehicleId)
                                                        .OrderByDescending(a => a.CreatedDate)
                                                        .SelectMany(a => a.TrackingPoints)
                                                        .Select(a => new CurrentPositionDto() {
                                                            Latitude = a.Latitude,
                                                            Longitude = a.Longitude,
                                                            PositionDateTime = a.CreatedDate,
                                                            VehicleId = request.VehicleId
                                                        })
                                                        .FirstOrDefault();

            if (latestTrackingPoint == null)
            {
                throw new LatestSnapshotNotFoundException($"Latest snapshot can be found by {request.VehicleId}.");
            }

            latestTrackingPoint.MatchingLocality = await _geocodingService.GetAddressByLatLong(latestTrackingPoint.Latitude, latestTrackingPoint.Longitude);

            return latestTrackingPoint;
        }
    }
}
