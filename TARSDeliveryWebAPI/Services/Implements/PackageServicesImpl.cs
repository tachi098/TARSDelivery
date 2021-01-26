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
    public class PackageServicesImpl : IPackageServices
    {
        private readonly ApplicationContext context;

        public PackageServicesImpl(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreatePackage(Package package)
        {
            await context.GetPackages.AddAsync(package);
            var created = await context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Package> GetNewPackage()
        {
            var models = await context.GetPackages.ToListAsync();
            return models[models.Count - 1];
        }
    }
}
