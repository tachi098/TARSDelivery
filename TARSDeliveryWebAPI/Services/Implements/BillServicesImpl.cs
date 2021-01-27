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
    public class BillServicesImpl : IBillServices
    {
        private readonly ApplicationContext context;

        public BillServicesImpl(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateBill(Bill bill)
        {
            await context.GetBills.AddAsync(bill);
            var created = await context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<IEnumerable<BillPackage>> GetBillPackages()
        {
            return await context.GetBills
                .Join(context.GetPackages,
                    b => b.PackageId,
                    p => p.Id,
                    (b, p) => new BillPackage
                    {
                        BillId = b.Id, // get
                        BillAccountId = b.AccountId,
                        BillPackageId = b.PackageId,
                        BillCreate_at = b.Create_at, // get
                        BillUpdate_at = b.Update_at,
                        BillDelete_at = b.Delete_at,

                        PackageId = p.Id,
                        PackageTitle = p.Title, // get
                        PackageNameFrom = p.NameFrom, // get
                        PackageEmail = p.Email, // get
                        PackageAddressFrom = p.AddressFrom,
                        PackageType = p.Type, // get
                        PackageZipCode = p.ZipCode,
                        PackageNameTo = p.NameTo,
                        PackageAddressTo = p.AddressTo,
                        PackageWeight = p.Weight,
                        PackageDistance = p.Distance,
                        PackageMessage = p.Message,
                        PackageTotalPrice = p.TotalPrice, // get
                        PackageStatus = p.Status, // get
                        PackageBranchId = p.BranchId,
                        PackageAccountId = p.AccountId,
                        PackageCreate_at = p.Create_at,
                        PackageUpdate_at = p.Update_at,
                        PackageDelete_at = p.Delete_at,
                    }).Where(m => m.BillDelete_at == null && m.PackageDelete_at == null).ToListAsync();
        }
    }
}
