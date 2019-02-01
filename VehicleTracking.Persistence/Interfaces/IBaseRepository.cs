using VehicleTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace VehicleTracking.Persistence.Interfaces
{
    internal interface IBaseRepository<Aggregate> 
        where Aggregate : AggregateRoot
    {
        /// <summary>
        /// Returning list of entities base on expression
        /// </summary>
        /// <param name="expression">Condition for returning list of entities</param>
        /// <returns>Returning list of entities</returns>
        IQueryable<Aggregate> Read(Expression<Func<Aggregate, bool>> expression);

        /// <summary>
        /// Returning an entity base on expression
        /// </summary>
        /// <param name="expression">Condition for returning an entity</param>
        /// <returns>Returning an entity</returns>
        Aggregate ReadOne(Expression<Func<Aggregate, bool>> expression);

        /// <summary>
        /// Creating an entity.
        /// </summary>
        /// <param name="entity">Entity need to create</param>
        /// <returns>Returning a created entity </returns>
        Aggregate Create(Aggregate entity);

        /// <summary>
        /// Updating an entity
        /// </summary>
        /// <param name="entity">Entity for updating</param>
        int Update(Aggregate entity);

        /// <summary>
        /// Deleting list of entities base on condition
        /// </summary>
        /// <param name="expression">Condition for deleting list of entities</param>
        /// <returns>Returning number as result (1 as success, below 1 as fail)</returns>
        int Delete(Expression<Func<Aggregate, bool>> expression);
    }
}
