using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Seeders
{
    public class RoleSeeders
    {
        public RoleSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, AccountId = 1, Position = 1 },
                new Role { Id = 2, AccountId = 2, Position = 2 },
                new Role { Id = 3, AccountId = 3, Position = 3 },
                new Role { Id = 4, AccountId = 4, Position = 3 }
            );
        }
    }
}
