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
        public DbSet<Package> GetPackages { get; set; }
        public DbSet<Bill> GetBills { get; set; }
        public DbSet<Comment> GetComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Values
            /* PriceList */
            modelBuilder.Entity<PriceList>().Property(m => m.Create_at).HasDefaultValue(DateTime.Now);

            /* Account */
            modelBuilder.Entity<Account>().Property(m => m.Create_at).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Account>().Property(m => m.Code).HasDefaultValue(null);
            modelBuilder.Entity<Account>().Property(m => m.Avartar).HasDefaultValue("images/p1.png");

            /* Package */
            modelBuilder.Entity<Package>().Property(m => m.Create_at).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Package>().Property(m => m.Status).HasDefaultValue(1);
            modelBuilder.Entity<Package>().Property(m => m.Id).UseIdentityColumn(seed: int.Parse(DateTime.Now.ToString("yyyyMMdd")), increment: 1);

            /* Bill */
            modelBuilder.Entity<Bill>().Property(m => m.Create_at).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Bill>().Property(m => m.Id).UseIdentityColumn(seed: int.Parse(DateTime.Now.ToString("yyyyMMdd")), increment: 1);
            #endregion

            #region Relationship
            /* One-To-Many */
            // (Account - Bill)
            modelBuilder.Entity<Account>()
                .HasMany(a => a.GetBills)
                .WithOne(b => b.GetAccount);

            /* One-To-Many */
            // (Account - Package)
            modelBuilder.Entity<Account>()
                .HasMany(a => a.GetPackages)
                .WithOne(p => p.GetAccount);

            /* One-To-Many */
            // (Account - Branche)
            modelBuilder.Entity<Branch>()
                .HasMany(a => a.GetAccounts)
                .WithOne(b => b.GetBranch);

            /* One-To-Many */
            // (Branche - Package)
            modelBuilder.Entity<Branch>()
                .HasMany(b => b.GetPackages)
                .WithOne(p => p.GetBranch);

            /* One-To-One*/
            // (Account - Role)
            modelBuilder.Entity<Account>()
                .HasOne(a => a.GetRole)
                .WithOne(r => r.GetAccount)
                .HasForeignKey<Role>(r => r.AccountId);

            /* One-To-One */
            // (Package - Bill)
            modelBuilder.Entity<Package>()
                .HasOne(p => p.GetBill)
                .WithOne(b => b.GetPackage)
                .HasForeignKey<Bill>(b => b.PackageId);
            #endregion


            /* Create Seeders */
            new ApplicationSeeders().OnModelSeeders(modelBuilder);
        }
    }
}
