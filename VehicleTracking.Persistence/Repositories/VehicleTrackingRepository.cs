﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VehicleTracking.Domain.Entities;
using VehicleTracking.Persistence.Interfaces;

namespace VehicleTracking.Persistence.Repositories
{
    public class VehicleTrackingRepository<Aggregate> : IVehicleTrackingRepository<Aggregate>
        where Aggregate : AggregateRoot
    {
        private readonly VehicleTrackingDbContext _vehicleDbContext;

        public VehicleTrackingRepository(VehicleTrackingDbContext vehicleDbContext)
        {
            _vehicleDbContext = vehicleDbContext;
        }

        public IQueryable<Aggregate> Read(Expression<Func<Aggregate, bool>> expression)
        {
            var result = _vehicleDbContext.Set<Aggregate>()
                                            .Where(expression)
                                            .AsNoTracking();

            return result;
        }

        public Aggregate ReadOne(Expression<Func<Aggregate, bool>> expression)
        {
            var entity = _vehicleDbContext.Set<Aggregate>()
                                            .AsNoTracking()
                                            .FirstOrDefault(expression);

            return entity;
        }

        public Aggregate Create(Aggregate entity)
        {
            if (entity != null)
            {
                _vehicleDbContext.Set<Aggregate>().Add(entity);

                AddLog(entity.Id, AuditAction.Create, entity.GetType().Name, JsonConvert.SerializeObject(entity));

                if (_vehicleDbContext.SaveChanges() > 0)
                {
                    return entity;
                }
            }
           
            return null;
        }

        public int Update(Aggregate updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ArgumentNullException("updated Entity was null.");
            }

            var entity = ReadOne(a => a.Id.Equals(updatedEntity.Id));
            if (entity != null)
            {
                _vehicleDbContext.Entry<Aggregate>(updatedEntity).State = EntityState.Modified;

                AddLog(entity.Id, AuditAction.Update, entity.GetType().Name, JsonConvert.SerializeObject(entity), JsonConvert.SerializeObject(updatedEntity));

                return _vehicleDbContext.SaveChanges();
            }
            throw new KeyNotFoundException("Entity not found.");
        }

        public int Delete(Expression<Func<Aggregate, bool>> expression)
        {
            var entities = _vehicleDbContext.Set<Aggregate>().Where(expression);

            foreach (var entity in entities)
            {
                _vehicleDbContext.Set<Aggregate>().Remove(entity);
                AddLog(entity.Id, AuditAction.Delete, entity.GetType().Name, "", JsonConvert.SerializeObject(entity));
            }

            return _vehicleDbContext.SaveChanges();
        }

        private void AddLog(Guid entityId, AuditAction action, string entityName, string updatedData, string oldData = "")
        {
            //Avoid adding duplicated log.
            if (typeof(AuditAction) != typeof(AuditLogs))
            {
                AuditLogs log = new AuditLogs()
                {
                    Action = action,
                    EntitiesName = entityName,
                    UpdatedData = updatedData,
                    EntityId = entityId
                };

                _vehicleDbContext.Entry<AuditLogs>(log).State = EntityState.Added;
            }

        }

        public bool IsAny(Expression<Func<Aggregate, bool>> expression)
        {
            return _vehicleDbContext.Set<Aggregate>().Any(expression);
        }
    }
}
