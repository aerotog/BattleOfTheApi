using System;
using System.Collections.Generic;
using System.Linq;
using BOTA.Core.Models;
using Microsoft.EntityFrameworkCore.Internal;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace BOTA.Core.Repository
{
    public static class SeedData
    {
        public static void EnsureSeedData(this ShopContext db)
        {
            if (!EnumerableExtensions.Any(db.Products))
            {
                AddProducts(db);
            }

            if (!EnumerableExtensions.Any(db.Users))
            {
                AddUsers(db);
            }

            if (!EnumerableExtensions.Any(db.Orders))
            {
                AddOrders(db);
            }
        }

        private static void AddProducts(ShopContext db)
        {
            var products = new List<Product>();
            var random = new Random();
            var wordGenerator = RandomizerFactory.GetRandomizer(new FieldOptionsTextWords());
            for (int i = 0; i < 10; i++)
            {
                var name = wordGenerator.Generate().Split(" ").First();
                products.Add(new Product
                {
                    Name = name,
                    Price = random.Next(1000)
                });
            }

            db.Products.AddRange(products);
            db.SaveChanges();
        }
        
        private static void AddUsers(ShopContext db)
        {
            var users = new List<User>();
            var firstNameGenerator = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
            var lastNameGenerator = RandomizerFactory.GetRandomizer(new FieldOptionsLastName());
            var cityGenerator = RandomizerFactory.GetRandomizer(new FieldOptionsCity());

            for (int i = 0; i < 10; i++)
            {
                users.Add(new User
                {
                    FirstName = firstNameGenerator.Generate(),
                    LastName = lastNameGenerator.Generate(),
                    City = cityGenerator.Generate()
                });
            }

            db.Users.AddRange(users);
            db.SaveChanges();
        }
        
        private static void AddOrders(ShopContext db)
        {
            var orders = new List<Order>();
            var random = new Random();
            var products = db.Products.ToList();

            var maxOrdersPerUser = 3;
            var maxItemsPerOrder = 5;
            var maxQuantityPerItem = 5;
            foreach (var userId in db.Users.Select(x => x.Id).ToList())
            {
                for (int i = 0; i < random.Next(maxOrdersPerUser); i++)
                {
                    var items = new List<Item>();
                    for (int j = 0; j < random.Next(maxItemsPerOrder); j++)
                    {
                        items.Add(new Item
                        {
                            Quantity = random.Next(maxQuantityPerItem),
                            Product = products[random.Next(products.Count)]
                        });
                    }
                    orders.Add(new Order
                    {
                        Date = DateTime.UtcNow.AddHours(-random.Next(7*24)),
                        Items = items,
                        UserId = userId
                    });
                }
            }

            db.Orders.AddRange(orders);
            db.SaveChanges();
        }
    }
}
