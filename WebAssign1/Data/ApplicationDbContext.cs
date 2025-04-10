using Microsoft.EntityFrameworkCore;
using WebAssign1.Models;

namespace WebAssign1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Monaco", Price = 10, Quantity = 10 },
                new Product { Id = 2, Name = "Bounce", Price = 20, Quantity = 15 },
                new Product { Id = 3, Name = "Good-Day", Price = 25, Quantity = 20 }
                );
        }
    }
}
