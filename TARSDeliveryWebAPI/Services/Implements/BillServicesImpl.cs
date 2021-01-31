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
    public class BillServicesImpl : IBillServices
    {
        private readonly ApplicationContext context;

        public BillServicesImpl(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateBill(Bill bill)
        {
            bill.Create_at = DateTime.Now;
            await context.GetBills.AddAsync(bill);
            var created = await context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteBill(int id)
        {
            var model = await context.GetBills.FindAsync(id);
            model.Delete_at = DateTime.Now;
            context.GetBills.Update(model);
            var deleted = await context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Bill> GetBill(int id)
        {
            return await context.GetBills.FindAsync(id);
        }

        public async Task<IEnumerable<BillPackage>> GetBillPackages()
        {
            return await context.GetBills
                .Join(context.GetPackages,
                    b => b.PackageId,
                    p => p.Id,
                    (b, p) => new BillPackage
                    {
                        GetBill = b,
                        GetPackage = p
                    }).ToListAsync();
        }

        public async Task<bool> UndoBill(int id)
        {
            var model = await context.GetBills.FindAsync(id);
            model.Delete_at = null;
            context.GetBills.Update(model);
            var undo = await context.SaveChangesAsync();
            return undo > 0;
        }

        public async Task<bool> UpdateBill(Bill bill)
        {
            bill.Update_at = DateTime.Now;
            context.GetBills.Update(bill);
            var updated = await context.SaveChangesAsync();
            return updated > 0;
        }
    }
}
