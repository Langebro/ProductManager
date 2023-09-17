using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProductManager.Domain;

namespace ProductManager.Data
{
    internal class AppliciationDbContext : DbContext
    {
        private string connectionString = "Server=.;Database=ProductManagwer;Integrated Security=true;Encrypt=False";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }


        public DbSet<Product> Product { get; set; }
    }
}
