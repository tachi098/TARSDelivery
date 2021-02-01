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
    public class AccountServicesImpl : IAccountServices
    {
        private readonly ApplicationContext context;
        public AccountServicesImpl(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<Account> GetAccount(int id)
        {
            return await context.GetAccounts.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await context.GetAccounts.ToListAsync();
        }

        public async Task<bool> UpdateAccount(Account account)
        {
            context.Update(account);
            var updated = await context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> CreateAccount(Account account)
        {
            context.Add(account);
            var added = await context.SaveChangesAsync();
            return added > 0;
        }

        public async Task<bool> DeleteAccount(int id)
        {
            var model = await context.GetAccounts.FindAsync(id);
            model.Delete_at = DateTime.Now;
            context.GetAccounts.Update(model);
            var deleted = await context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<IEnumerable<BillPackageAccount>> billPackageAccounts()
        {
  
            var models = await context.GetPackages.Join(context.GetAccounts,

                p => p.AccountId,
                a => a.Id,                                               // //Package  and account
                (p, a) => new BillPackageAccount
                {
                    GetPackage = p,
                    GetAccount = a
                }).Where(m =>m.GetPackage.AccountId == m.GetAccount.Id).ToListAsync();

            return models;
        }
    }
}
