﻿using Microsoft.EntityFrameworkCore;
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
                new PriceList { Id = 1, Name = "VPP", PriceDistance = 1.08, PriceWeight = 1.21, Create_at = DateTime.Now }
            );
        }
    }
}
