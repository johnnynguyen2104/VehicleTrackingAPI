using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VehicleTracking.Domain.Entities
{
    public abstract class AggregateRoot
    {
        [Column( TypeName = "varchar(50)")]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public Nullable<DateTime> UpdatedDateTime { get; set; }
    }
}
