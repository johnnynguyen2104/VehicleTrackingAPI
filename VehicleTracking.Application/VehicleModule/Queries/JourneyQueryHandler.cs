using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class JourneyQueryHandler : IRequestHandler<JourneyQuery, JourneyDto>
    {
        private readonly ITrackingRepository<TrackingPoints> _trackingPointRepository;
        private readonly ITrackingRepository<TrackingPointSnapshots> _trackingSnapshotRepository;
        private readonly IVehicleTrackingRepository<Vehicle> _vehicleRepository;
        private readonly IGeocodingService _geocodingService;
        private readonly IDateTime _machineDateTime;

        public JourneyQueryHandler(ITrackingRepository<TrackingPoints> trackingPointRepository
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

        public async Task<JourneyDto> Handle(JourneyQuery request, CancellationToken cancellationToken)
        {
            //Checking actived Vehicle.
            if (!_vehicleRepository.IsAny(a => a.IsActive
                                            && a.Id == request.VehicleId
                                            && a.DeviceCode == request.DeviceCode
                                            && a.ActivatedCode == request.ActivatedCode))
            {
                throw new VehicleNotFoundException($"Vehicle not found with Id {request.VehicleId} or the Vehicle was deactived.");
            }

            //Firstly I get snapshots in a certain time (from date 00:00:00  to date +1 00:00:00), 
            // then I will inner join with tracking point table to get tracking points in certain of time. 
            var journey = _trackingSnapshotRepository.Read(a => (a.CreatedDate >= request.FromDateTime.Date && a.CreatedDate < request.ToDateTime.AddDays(1).Date)
                                                                    && a.VehicleReferencedCode == request.VehicleId)
                                                                 .Include(a => a.TrackingPoints)
                                                                 .SelectMany(a => a.TrackingPoints)
                                                                 .Where(a => (a.CreatedDate >= request.FromDateTime && a.CreatedDate <= request.ToDateTime))
                                                                 .OrderBy(a => a.CreatedDate)
                                                                 .Select(a => new PositionDto()
                                                                 {
                                                                     Latitude = a.Latitude,
                                                                     Longitude = a.Longitude,
                                                                     PositionDateTime = a.CreatedDate
                                                                 }).ToList();
            if (journey == null)
            {
                throw new JourneyNotFoundException($"Journey not found for vehicleid {request?.VehicleId} between {request.FromDateTime} and {request.ToDateTime}");
            }

            var latestTrackingPoint = new JourneyDto()
            {
                Positions = journey,
                VehicleId = request.VehicleId
            };

            List<Task> tasks = new List<Task>(latestTrackingPoint.Positions.Count);
            foreach (var p in latestTrackingPoint.Positions)
            {
                tasks.Add(FillMatchingLocality(p));
            }

            await Task.WhenAll(tasks);

            return latestTrackingPoint;
        }

        private async Task FillMatchingLocality(PositionDto positions)
        {
            positions.MatchingLocality = await _geocodingService.GetAddressByLatLong(positions.Latitude, positions.Longitude);
        }
    }
}
