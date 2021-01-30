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
            account.Create_at = DateTime.Now;
            context.Add(account);
            var added = await context.SaveChangesAsync();
            return added > 0;
        }

    }
}
