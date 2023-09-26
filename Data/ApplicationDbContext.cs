using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProductManager.Domain;

namespace ProductManager.Data
{
    internal class ApplicationDbContext : DbContext
    {
        private string connectionString = "Server=.;Database=ProductManager;Integrated Security=true;Encrypt=False";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }


        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }   

        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
