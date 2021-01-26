using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;
using TARSDeliveryWebAPI.Repositories;
using TARSDeliveryWebAPI.Services.Interfaces;

namespace TARSDeliveryWebAPI.Services.Implements
{
    public class BillServicesImpl : IBillServices
    {
        private readonly ApplicationContext context;

        public BillServicesImpl(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateBill(Bill bill)
        {
            await context.GetBills.AddAsync(bill);
            var created = await context.SaveChangesAsync();
            return created > 0;
        }
    }
}
