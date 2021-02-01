using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;
using TARSDeliveryWebAPI.Services.Interfaces;
using TARSDeliveryWebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
namespace TARSDeliveryWebAPI.Services.Implements
{
    public class BranchServicesImpl : IBranchServices
    {
        private ApplicationContext context;
        public BranchServicesImpl(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Branch>> GetBranches()
        {
            return await context.GetBranches.ToListAsync();
        }

        public async Task<Branch> PostBranch(Branch branch)
        {
            var model = await context.GetBranches.SingleOrDefaultAsync(c => c.Id == branch.Id);
            if(model == null)
            {
                context.GetBranches.Add(branch);
              await  context.SaveChangesAsync();
               
            }
            return model;
        }

        public async Task<bool> DeleteBranch(int id)
        {
            var model = await context.GetBranches.FindAsync(id);
            model.Delete_at = DateTime.Now;
            context.GetBranches.Update(model);
            var deleted = await context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> Edit(Branch branch)
        {
            branch.Update_at = DateTime.Now;
            context.GetBranches.Update(branch);
            var updated = await context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<Branch> GetBranch(int id)
        {
            return await context.GetBranches.SingleOrDefaultAsync(c => c.Id == id);

        }
    }
}
