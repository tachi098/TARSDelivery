using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Seeders
{
    public class PriceListSeeders
    {
        public PriceListSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceList>().HasData(
                new PriceList { Id = 1, Name = "VPP", PriceDistance = 1.08, PriceWeight = 0, Create_at = DateTime.Now },
                new PriceList { Id = 2, Name = "Normal", PriceDistance = 0, PriceWeight = 1.21, Create_at = DateTime.Now },
                new PriceList { Id = 3, Name = "Speed", PriceDistance = 0, PriceWeight = 1.35, Create_at = DateTime.Now },
                new PriceList { Id = 4, Name = "Courier", PriceDistance = 0, PriceWeight = 1.46, Create_at = DateTime.Now }
            );
        }
    }
}
