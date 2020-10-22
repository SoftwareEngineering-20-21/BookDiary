using BookDiary.DAL.Entities;
using System.Data.Entity;

namespace BookDiary.DAL.EF
{
    public class AppDbContext : DbContext
    { 
        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        public AppDbContext() : base("DefaultConnection") { }

        public AppDbContext(string connectionString) : base(connectionString) { }
    }
}