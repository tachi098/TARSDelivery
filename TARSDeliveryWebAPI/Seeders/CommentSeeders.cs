using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Seeders
{
    public class CommentSeeders
    {
        public CommentSeeders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, FullName = "Taylor Swift", Email = "Taylor@gmail.com", Body = "This such a nice delivery page, I love it", Create_at = DateTime.Now },
                new Comment { Id = 2, FullName = "Selena Gomez", Email = "GomezSinger@gmail.com", Body = "Everything is perfect", Create_at = DateTime.Now },
                new Comment { Id = 3, FullName = "Halsey", Email = "HalseyShazam@gmail.com", Body = "Delivery packages very fast, I don't know how to say, I appreciate it", Create_at = DateTime.Now },
                new Comment { Id = 4, FullName = "Alan Walker", Email = "AlanWalker@gmail.com", Body = "I love how this page work", Create_at = DateTime.Now }
            );
        }
    }
}
