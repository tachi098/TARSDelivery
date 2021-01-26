using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Seeders
{
    public class BranchSeeders
    {
        public BranchSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    Id = 1,
                    Name = "Branch 01",
                    Address = "Address 01",
                    Phone = "0901858004"
                }
            );
        }
    }
}
