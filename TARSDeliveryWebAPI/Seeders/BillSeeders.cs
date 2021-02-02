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
                },
                new Bill 
                { 
                    Id = 20210129,
                    PackageId = 20210129
                },
                new Bill
                {
                    Id = 20210130,
                    PackageId = 20210130
                },
                new Bill
                {
                    Id = 20210131,
                    PackageId = 20210131
                },
                new Bill
                {
                    Id = 20210132,
                    PackageId = 20210132
                }
            );
        }
    }
}
