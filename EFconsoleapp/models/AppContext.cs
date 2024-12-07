using Microsoft.EntityFrameworkCore;
using System;
using FirstApp.Models;
using FirstApp.Repositories;

namespace FirstApp
{
    public class AppContext : DbContext
    {
        // Объекты таблицы Users
        public DbSet<UserLibrary> Users { get; set; }
        public DbSet<BookLibrary> Books { get; set; }

        public AppContext()
        {
            var f = Database.EnsureCreated();
            Console.WriteLine($"Database {f}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:localhost,1433;Database=testing;User ID=sa;Password=1234Bert@;Trusted_Connection=False;Encrypt=False;");
        }
    }
}
