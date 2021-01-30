using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Services.Interfaces
{
    public interface IAccountServices
    {
        Task<IEnumerable<Account>> GetAccounts();

        Task<Account> GetAccount(int id);

        Task<bool> UpdateAccount(Account account);

        Task<bool> CreateAccount(Account account);

    }
}
