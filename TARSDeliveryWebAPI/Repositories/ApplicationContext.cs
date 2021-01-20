using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Repositories
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        public DbSet<Account> Account { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<PriceList> PriceList { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetail> BillDetail { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}
