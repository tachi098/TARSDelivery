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
    public class RoleServicesImpl: IRoleServices
    {
        private readonly ApplicationContext context;

        public RoleServicesImpl(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Role> GetRole(int accountid)
        {
            return await context.GetRoles.SingleOrDefaultAsync(m => m.AccountId == accountid);
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await context.GetRoles.ToListAsync();
        }

        public async Task<bool> CreateRole(Role role)
        {
            context.Add(role);
            var added = await context.SaveChangesAsync();
            return added > 0;
        }
    }
}
