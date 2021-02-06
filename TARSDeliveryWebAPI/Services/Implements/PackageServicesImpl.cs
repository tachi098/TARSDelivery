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
            package.Create_at = DateTime.Now;
            await context.GetPackages.AddAsync(package);
            var created = await context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeletePackage(int id)
        {
            var model = await context.GetPackages.FindAsync(id);
            model.Delete_at = DateTime.Now;
            context.GetPackages.Update(model);
            var deleted = await context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Package> GetNewPackage()
        {
            var models = await context.GetPackages.ToListAsync();
            return models[models.Count - 1];
        }

        public async Task<Package> GetPackage(int id)
        {
            return await context.GetPackages.FindAsync(id);
        }

        public async Task<IEnumerable<Package>> GetPackages()
        {
            var model = await context.GetPackages.ToListAsync();
            return model;
        }

        public async Task<bool> UndoPackage(int id)
        {
            var model = await context.GetPackages.FindAsync(id);
            model.Delete_at = null;
            context.GetPackages.Update(model);
            var undo = await context.SaveChangesAsync();
            return undo > 0;
        }

        public async Task<bool> UpdatePackage(Package package)
        {
            package.Update_at = DateTime.Now;
            context.GetPackages.Update(package);
            var updated = await context.SaveChangesAsync();
            return updated > 0;
        }
    }
}
