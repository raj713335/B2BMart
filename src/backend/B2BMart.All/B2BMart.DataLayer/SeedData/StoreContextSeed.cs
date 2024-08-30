using B2BMart.DataLayer.Models;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace B2BMart.DataLayer.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(B2BMartContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var path = @"./../B2BMart.DataLayer/SeedData/";
                //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!context.Users.Any())
                {
                    using var hmac = new HMACSHA512();
                    var userData = File.ReadAllText(path + @"user.json");
                    var users = JsonSerializer.Deserialize<List<User>>(userData);

                    foreach (var item in users)
                    {
                        item.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes((item.PasswordHash).ToString()));
                        item.PasswordSalt = hmac.Key;
                        Console.WriteLine(item.PasswordSalt);
                        context.Users.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Addresses.Any())
                {
                    var addressData = File.ReadAllText(path + @"address.json");
                    var address = JsonSerializer.Deserialize<List<Address>>(addressData);

                    foreach (var item in address)
                    {
                        context.Addresses.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText(path + @"brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(path + @"types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText(path + @"products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.DeliveryMethods.Any())
                {
                    var dmData = File.ReadAllText(path + @"delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach (var item in methods)
                    {
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Orders.Any())
                {
                    var ordersData = File.ReadAllText(path + @"orders.json");
                    var orders = JsonSerializer.Deserialize<List<Order>>(ordersData);

                    foreach (var item in orders)
                    {
                        context.Orders.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.OrderItems.Any())
                {
                    var ordersItemData = File.ReadAllText(path + @"ordersItem.json");
                    var ordersItem = JsonSerializer.Deserialize<List<OrderItem>>(ordersItemData);

                    foreach (var item in ordersItem)
                    {
                        context.OrderItems.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "Seed data migration failed");
            }
        }
    }
}
