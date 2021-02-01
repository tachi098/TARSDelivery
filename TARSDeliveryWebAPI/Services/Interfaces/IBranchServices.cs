using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using TARSDeliveryWebAPI.Models;
using TARSDeliveryWebAPI.Repositories;
namespace TARSDeliveryWebAPI.Services.Interfaces
{
   public interface IBranchServices
    {
        Task<IEnumerable<Branch>> GetBranches();
        Task<Branch> PostBranch(Branch branch);
        Task<Branch> GetBranch(int id);

        Task<bool> Edit(Branch branch);
        Task<bool> DeleteBranch(int id);
    }
}
