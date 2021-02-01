using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Services.Interfaces
{
    public interface IBillServices
    {
        Task<bool> CreateBill(Bill bill);
        Task<IEnumerable<BillPackageAccount>> GetBillPackages();
        Task<bool> DeleteBill(int id);
        Task<bool> UpdateBill(Bill bill);
    }
}
