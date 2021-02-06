using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebAPI.Seeders
{
    public class ApplicationSeeders
    {
        public void OnModelSeeders(ModelBuilder modelBuilder)
        {
            // PriceList
            new PriceListSeeders(modelBuilder);

            // Branch
            new BranchSeeders(modelBuilder);

            // Account
            new AccountSeeders(modelBuilder);

            // Role
            new RoleSeeders(modelBuilder);

            // Package
            new PackageSeeders(modelBuilder);

            // Bill
            new BillSeeders(modelBuilder);

            // Comment
            new CommentSeeders(modelBuilder);
        }
    }
}
