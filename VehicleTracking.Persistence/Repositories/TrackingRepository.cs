using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VehicleTracking.Domain.TrackingEntities;
using VehicleTracking.Persistence.Interfaces;

namespace VehicleTracking.Persistence.Repositories
{
    public class TrackingRepository<Aggregate>: ITrackingRepository<Aggregate>
        where Aggregate : AggregateTrackingRoot
    {
        private readonly TrackingDbContext _trackingDbContext;

        public TrackingRepository(TrackingDbContext trackingDbContext)
        {
            _trackingDbContext = trackingDbContext;
        }

        public IQueryable<Aggregate> Read(Expression<Func<Aggregate, bool>> expression)
        {
            var result = _trackingDbContext.Set<Aggregate>()
                                            .Where(expression)
                                            .AsNoTracking();

            return result;
        }

        public Aggregate ReadOne(Expression<Func<Aggregate, bool>> expression)
        {
            var entity = _trackingDbContext.Set<Aggregate>()
                                            .AsNoTracking()
                                            .FirstOrDefault(expression);

            return entity;
        }

        public Aggregate Create(Aggregate entity)
        {
            if (entity != null)
            {
                _trackingDbContext.Entry(entity).State = EntityState.Added;

                if (_trackingDbContext.SaveChanges() > 0)
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
                _trackingDbContext.Entry<Aggregate>(updatedEntity).State = EntityState.Modified;
        
                return _trackingDbContext.SaveChanges();
            }
            throw new KeyNotFoundException("Entity not found.");
        }

        public int Delete(Expression<Func<Aggregate, bool>> expression)
        {
            var entities = _trackingDbContext.Set<Aggregate>().Where(expression);

            foreach (var entity in entities)
            {
                _trackingDbContext.Set<Aggregate>().Remove(entity);
            }

            return _trackingDbContext.SaveChanges();
        }

        public bool IsAny(Expression<Func<Aggregate, bool>> expression)
        {
            return _trackingDbContext.Set<Aggregate>().Any(expression);
        }
    }
}
