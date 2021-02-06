using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Services.Interfaces
{
    public interface IPackageServices
    {
        Task<bool> CreatePackage(Package package);
        Task<Package> GetNewPackage();
        Task<bool> DeletePackage(int id);
        Task<Package> GetPackage(int id);
        Task<bool> UpdatePackage(Package package);
        Task<bool> UndoPackage(int id);
        Task<IEnumerable<Package>> GetPackages();
    }
}
