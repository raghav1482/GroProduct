using Microsoft.EntityFrameworkCore;

namespace GroProduct.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Order> Orders { get; set; }

        public DbSet<Models.OrderItem> OrderItems { get; set; }

    }
}
