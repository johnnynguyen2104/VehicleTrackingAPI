using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracking.Application.Exceptions;
using VehicleTracking.Common;
using VehicleTracking.Domain.Entities;
using VehicleTracking.Domain.TrackingEntities;
using VehicleTracking.Persistence.Interfaces;
using VehicleTracking.Persistence.Repositories;

namespace VehicleTracking.Application.VehicleModule.Commands
{
    public class RecordPositionCommandHandler : IRequestHandler<RecordPositionCommand>
    {
        private readonly ITrackingRepository<TrackingPoints> _trackingPointRepository;
        private readonly ITrackingRepository<TrackingPointSnapshots> _trackingSnapshotRepository;

        private readonly IVehicleTrackingRepository<Vehicle> _vehicleRepository;

        private readonly IDateTime _machineDateTime;

        public RecordPositionCommandHandler(ITrackingRepository<TrackingPoints> trackingPointRepository
            , ITrackingRepository<TrackingPointSnapshots> trackingSnapshotRepository
            , IVehicleTrackingRepository<Vehicle> vehicleRepository
            , IDateTime machineDateTime)
        {
            _trackingPointRepository = trackingPointRepository;
            _trackingSnapshotRepository = trackingSnapshotRepository;
            _vehicleRepository = vehicleRepository;
            _machineDateTime = machineDateTime;
        }


        public Task<Unit> Handle(RecordPositionCommand request, CancellationToken cancellationToken)
        {
            //Checking actived Vehicle.
            if (!_vehicleRepository.IsAny(a => a.IsActive 
                                            && a.Id == request.VehicleId
                                            && a.DeviceCode == request.DeviceCode
                                            && a.ActivatedCode == request.ActivatedCode))
            {
                throw new VehicleNotFoundException($"Vehicle not found with Id {request.VehicleId} or the Vehicle was deactived.");
            }

            
            var snapshotId = _trackingSnapshotRepository.Read(a => a.CreatedDate >= _machineDateTime.UtcNow.Date 
                                                                    && a.VehicleReferencedCode == request.VehicleId)
                                                        .Select(a => a.Id)
                                                        .FirstOrDefault();

            if (snapshotId == null)
            {
                #region Creating Snapshot if not existed for today.
                var trackingPoint = new TrackingPoints()
                {
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                };

                var newSnapshot = new TrackingPointSnapshots()
                {
                    VehicleReferencedCode = request.VehicleId,
                    TrackingPoints = new Collection<TrackingPoints>(),
                    StartPointId = trackingPoint.Id
                };

                newSnapshot.TrackingPoints.Add(trackingPoint);

                _trackingSnapshotRepository.Create(newSnapshot);
                #endregion
            }
            else
            {
                #region add a new point to snapshot
                var trackingPoint = new TrackingPoints()
                {
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    SnapshotId = snapshotId
                };

                _trackingPointRepository.Create(trackingPoint);
                #endregion
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
