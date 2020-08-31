﻿using BOTA.Core;
using Microsoft.EntityFrameworkCore;

namespace BOTA.DB
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options)
        : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            //Database.Migrate();
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
