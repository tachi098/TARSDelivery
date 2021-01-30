using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Seeders
{
    public class PackageSeeders
    {
        public PackageSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>().HasData(
                new Package // VPP
                {
                    Id = 20210126,
                    Title = "Package 01",
                    NameFrom = "Name From 01",
                    Email = "mail01@gmail.com",
                    AddressFrom = "Address From 01",
                    Type = "Mail",
                    ZipCode = "1234",
                    NameTo = "Name To 01",
                    AddressTo = "Address To 01",
                    Weight = 0,
                    Distance = 234.23,
                    Message = "Message 01",
                    TotalPrice = 324.32,
                    PriceListName = "VPP",
                    Status = 1 // Store,
                    
                },
                new Package // VPP Account
                {
                    Id = 20210127,
                    Title = "Package 02",
                    NameFrom = "Name From 02",
                    Email = "mail02@gmail.com",
                    AddressFrom = "Address From 02",
                    Type = "Mail",
                    ZipCode = "1234",
                    NameTo = "Name To 02",
                    AddressTo = "Address To 02",
                    Weight = 0,
                    Distance = 234.23,
                    Message = "Message 02",
                    TotalPrice = 324.32,
                    PriceListName = "VPP",
                    Status = 2, // Doing
                    AccountId = 3
                },
                new Package // Branch 3 : 3: Speed
                {
                    Id = 20210128,
                    Title = "Package 02",
                    NameFrom = "Name From 02",
                    Email = "mail02@gmail.com",
                    AddressFrom = "Address From 02",
                    Type = "Mail",
                    ZipCode = "1234",
                    NameTo = "Name To 02",
                    AddressTo = "Address To 02",
                    Weight = 4.5,
                    Distance = 0,
                    Message = "Message 02",
                    TotalPrice = 324.32,
                    PriceListName = "Speed",
                    Status = 3, // Finish
                    BranchId = 1
                }
            );
        }
    }
}
