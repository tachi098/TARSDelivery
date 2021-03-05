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
                    Weight = 1500,
                    Distance = 304.23,
                    Message = "Message 01",
                    TotalPrice = 652.32,
                    PriceListName = "VPP",
                    Status = 1
                    
                },
                new Package // VPP Account
                {
                    Id = 20210127,
                    Title = "Package 02",
                    NameFrom = "Name From 02",
                    Email = "tulha@gmail.com",
                    AddressFrom = "Address From 02",
                    Type = "Mail",
                    ZipCode = "1234",
                    NameTo = "Name To 02",
                    AddressTo = "Address To 02",
                    Weight = 789.09,
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
                    Email = "tulha@gmail.com",
                    AddressFrom = "Address From 02",
                    Type = "Mail",
                    ZipCode = "1234",
                    NameTo = "Name To 02",
                    AddressTo = "Address To 02",
                    Weight = 40.785,
                    Distance = 0,
                    Message = "Message 02",
                    TotalPrice = 324.32,
                    PriceListName = "Speed",
                    Status = 3, // Finish
                    BranchId = 1
                },                
                
                new Package // VPP
                {
                    Id = 20210129,
                    Title = "Package 04",
                    NameFrom = "Name From 04",
                    Email = "tulha@gmail.com",
                    AddressFrom = "Address From 04",
                    Type = "Mail",
                    ZipCode = "1234",
                    NameTo = "Name To 04",
                    AddressTo = "Address To 04",
                    Weight = 760,
                    Distance = 234.23,
                    Message = "Message 04",
                    TotalPrice = 524.32,
                    PriceListName = "VPP",
                    Status = 1,
                    Create_at = DateTime.Parse("2/1/2021")
                },
                new Package // VPP Account
                {
                    Id = 20210130,
                    Title = "Package 05",
                    NameFrom = "Name From 05",
                    Email = "kiennt@gmail.com",
                    AddressFrom = "Address From 05",
                    Type = "Mail",
                    ZipCode = "1234",
                    NameTo = "Name To 05",
                    AddressTo = "Address To 05",
                    Weight = 0,
                    Distance = 234.23,
                    Message = "Message 05",
                    TotalPrice = 654.32,
                    PriceListName = "VPP",
                    Status = 2, // Doing
                    AccountId = 3,
                    Create_at = DateTime.Parse("2/2/2021")
                },
                new Package // Branch 3 : 3: Speed
                {
                    Id = 20210131,
                    Title = "Package 06",
                    NameFrom = "Name From 06",
                    Email = "kiennt@gmail.com",
                    AddressFrom = "Address From 06",
                    Type = "Mail",
                    ZipCode = "1234",
                    NameTo = "Name To 02",
                    AddressTo = "Address To 06",
                    Weight = 48.5,
                    Distance = 0,
                    Message = "Message 06",
                    TotalPrice = 324.32,
                    PriceListName = "Speed",
                    Status = 3, // Finish
                    BranchId = 1
                },
                new Package // Branch 3 : 3: Speed
                {
                    Id = 20210132,
                    Title = "Package 07",
                    NameFrom = "Name From 07",
                    Email = "kiennt@gmail.com",
                    AddressFrom = "Address From 07",
                    Type = "Mail",
                    ZipCode = "1234",
                    NameTo = "Name To 07",
                    AddressTo = "Address To 07",
                    Weight = 4999.5,
                    Distance = 0,
                    Message = "Message 06",
                    TotalPrice = 324.32,
                    PriceListName = "Speed",
                    Status = 4, // Error
                    BranchId = 1
                }
            );
        }
    }
}
