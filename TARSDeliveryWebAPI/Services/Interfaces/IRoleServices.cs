using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRole(int accountid);
        Task<bool> CreateRole(Role role);
    }
}
