using BookDiary.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookDiary.DAL.EF
{
    public class AppDbContext : DbContext
    { 
        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Statistic> Statistics { get; set; }

        public DbSet<Notification> Notifications { get; set; }


        public AppDbContext()
            : base(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(
            @"data source=localhost;Initial Catalog=BookDiary1;Trusted_Connection=True;").Options)
        {

            Database.EnsureCreated();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}