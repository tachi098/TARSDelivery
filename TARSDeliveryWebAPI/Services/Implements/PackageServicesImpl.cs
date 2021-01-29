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

        public async Task<IEnumerable<Package>> GetPackages()
        {
            var models = await context.GetPackages.ToListAsync();
            /*      return models[models.Count - 1];*/
            return models;
          
        }

        public async Task<Package> GetPackage(int code)
        {
            var model = await context.GetPackages.FirstOrDefaultAsync(c => c.Id.Equals(code));
            
            return model;
        }
    }
}
