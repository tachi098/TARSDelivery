using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Seeders
{
    public class BillSeeders
    {
        public BillSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>().HasData(
                new Bill 
                { 
                    Id = 20210126,
                    PackageId = 20210126
                },
                new Bill
                {
                    Id = 20210127,
                    PackageId = 20210127
                },
                new Bill
                {
                    Id = 20210128,
                    PackageId = 20210128
                }
            );
        }
    }
}
