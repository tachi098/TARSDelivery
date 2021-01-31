using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Services.Interfaces
{
    public interface IPriceListServices
    {
        Task<IEnumerable<PriceList>> priceLists();

        Task<bool> Edit(PriceList priceList);

        Task<bool> DeletePriceList(int id);

        Task<bool> Create(PriceList priceList);

        Task<PriceList> GetPriceList(string name);
        Task<IEnumerable<PriceList>> GetPriceLists();
    }
}
