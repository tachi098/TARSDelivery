using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Services.Interfaces
{
    public interface IPriceListServices
    {
        Task<PriceList> GetPriceList(string name);
        Task<IEnumerable<PriceList>> GetPriceLists();
    }
}
