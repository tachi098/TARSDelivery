using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Seeders
{
    public class AccountSeeders
    {
        public AccountSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                // Password = 12345678
                new Account { Id = 1, FullName = "Pham Quang Huy", Email = "toilahuy098@gmail.com", Password = "$2a$11$VkOpTPdd8aQPsDMQYA2qb.zfGKHV7AWELUCiLGW9VULaEVF3ItsFW", Address = "Binh Thanh", Phone = "0933691822", Birthday = DateTime.Parse("4/4/1998 12:00:00 AM"), Avartar = "images/p1.png", Create_at = DateTime.Now },
                new Account { Id = 2, FullName = "Pham Xuan Hoai", Email = "hoaixp@gmail.com", Password = "$2a$11$VkOpTPdd8aQPsDMQYA2qb.zfGKHV7AWELUCiLGW9VULaEVF3ItsFW", Address = "Binh Thanh", Phone = "0933691822", Birthday = DateTime.Parse("3/21/1998 12:00:00 AM"), Avartar = "images/p1.png", Create_at = DateTime.Now, BranchId = 1 },
                new Account { Id = 3, FullName = "Le Hoang Anh Tu", Email = "tulha@gmail.com", Password = "$2a$11$VkOpTPdd8aQPsDMQYA2qb.zfGKHV7AWELUCiLGW9VULaEVF3ItsFW", Address = "Binh Thanh", Phone = "0933691822", Birthday = DateTime.Parse("3/21/1998 12:00:00 AM"), Avartar = "images/p1.png", Create_at = DateTime.Now },
                new Account { Id = 4, FullName = "Ngo Trung Kien", Email = "kiennt@gmail.com", Password = "$2a$11$VkOpTPdd8aQPsDMQYA2qb.zfGKHV7AWELUCiLGW9VULaEVF3ItsFW", Address = "Binh Thanh", Phone = "0933691822", Birthday = DateTime.Parse("3/21/1998 12:00:00 AM"), Avartar = "images/p1.png", Create_at = DateTime.Now }
            );
        }
    }
}
