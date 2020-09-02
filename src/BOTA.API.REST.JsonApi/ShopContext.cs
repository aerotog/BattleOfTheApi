using BOTA.API.REST.JsonApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BOTA.API.REST.JsonApi
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        // JSON API requires special model attributes so we cannot re-use the common DbContext
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
