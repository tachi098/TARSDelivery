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

        public async Task<IEnumerable<PriceList>> priceLists() //Index
        {
            return await context.GetPriceLists.ToListAsync();
        }

        public async Task<bool> Edit(PriceList priceList) //Update
        {
            priceList.Update_at = DateTime.Now;
            context.GetPriceLists.Update(priceList);
            var edited = await context.SaveChangesAsync();
            return edited > 0;
        }

        public async Task<bool> DeletePriceList(int id) //Delete
        {
            var model = await context.GetPriceLists.FindAsync(id);

            model.Delete_at = DateTime.Now;

            context.GetPriceLists.Update(model);
            var deleted = await context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> Create(PriceList priceList) //Create
        {
            priceList.Create_at = DateTime.Now;
            context.GetPriceLists.Add(priceList);
            var created = await context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<IEnumerable<PriceList>> GetPriceLists()
        {
            return await context.GetPriceLists.ToListAsync();
        }

        public async Task<PriceList> GetPriceList(string name)
        {
            return await context.GetPriceLists.SingleOrDefaultAsync(m => m.Name.Equals(name));
        }
    }
}
