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
    }
}
