using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;
using TARSDeliveryWebAPI.Seeders;

namespace TARSDeliveryWebAPI.Repositories
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        public DbSet<Account> GetAccounts { get; set; }
        public DbSet<Role> GetRoles { get; set; }
        public DbSet<PriceList> GetPriceLists { get; set; }
        public DbSet<Branch> GetBranches { get; set; }
        public DbSet<Bill> GetBills { get; set; }
        public DbSet<Comment> GetComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* PriceList */
            modelBuilder.Entity<PriceList>().Property(m => m.Create_at).HasDefaultValue(DateTime.Now);

            /* Account */
            modelBuilder.Entity<Account>().Property(m => m.Create_at).HasDefaultValue(DateTime.Now);

            /* Bill */
            modelBuilder.Entity<Bill>().Property(m => m.Create_at).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Bill>().Property(m => m.Status).HasDefaultValue(1);

            /* Create Seeders */
            new ApplicationSeeders().OnModelSeeders(modelBuilder);
        }
    }
}
