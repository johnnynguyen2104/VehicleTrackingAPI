using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Domain.Entities
{
    public class UsersSystem : IdentityUser<Guid>
    {
        public DateTime CreatedDateTime { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public UsersSystem()
        {
            Id = Guid.NewGuid();
            CreatedDateTime = DateTime.UtcNow;
        }
    }
}
