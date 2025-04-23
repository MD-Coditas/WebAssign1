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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Monaco", Price = 10, Quantity = 10, ImageUrl = "https://regalplus.com/cdn/shop/files/monacoyellow.jpg?v=1736527560" },
                new Product { Id = 2, Name = "Bounce", Price = 20, Quantity = 15, ImageUrl = "https://5.imimg.com/data5/SELLER/Default/2021/5/RQ/IA/NE/34912835/bounce-choco.jpg" },
                new Product { Id = 3, Name = "Good-Day", Price = 25, Quantity = 20, ImageUrl = "https://m.media-amazon.com/images/I/61kBRuYl3vL._AC_UF1000,1000_QL80_.jpg" }
                );
        }
    }
}
