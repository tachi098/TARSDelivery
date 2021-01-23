using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;
using TARSDeliveryWebAPI.Repositories;
using TARSDeliveryWebAPI.Services.Interfaces;

namespace TARSDeliveryWebAPI.Services.Implements
{
    public class PriceListServicesImpl : IPriceListServices
    {
        private readonly ApplicationContext context;

        public PriceListServicesImpl(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<PriceList> GetPriceList(string name)
        {
            return await context.GetPriceLists.SingleOrDefaultAsync(m => m.Name.Equals(name));
        }
    }
}
