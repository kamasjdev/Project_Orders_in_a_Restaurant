using Dapper;
using Restaurant.Domain.Entities;
using System;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Restaurant.Infrastructure.Migrations
{
    internal class SeedData
    {
        public static void AddData(IDbConnection dbConnection)
        {
            var result = dbConnection.Query<Migration>("SELECT Id, Name, Version FROM migrations WHERE 1=1 ORDER BY Version DESC LIMIT 1").SingleOrDefault();

            if (result is null)
            {
                AddPizzaWithAdditions(dbConnection);
                AddMainDishWithAdditions(dbConnection);
                AddSoups(dbConnection);
                AddDrinks(dbConnection);
                dbConnection.Execute("INSERT INTO migrations (Id, Name, Version) VALUES (@Id, @Name, @Version)",
                            new { Id = Guid.NewGuid(), Name = $"First_Migration_{DateTime.UtcNow.ToString("s", CultureInfo.GetCultureInfo("en-US"))}", Version = result?.Version + 1 ?? 1 });
            }
        }

        private static void AddDrinks(IDbConnection dbConnection)
        {
            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                        new { Id = Guid.NewGuid(), ProductName = "Kawa", Price = 100.50M, ProductKind = ProductKind.Drink });

            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                        new { Id = Guid.NewGuid(), ProductName = "Herbata", Price = 100.50M, ProductKind = ProductKind.Drink });

            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                        new { Id = Guid.NewGuid(), ProductName = "Cola", Price = 100.50M, ProductKind = ProductKind.Drink });
        }

        private static void AddSoups(IDbConnection dbConnection)
        {
            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                        new { Id = Guid.NewGuid(), ProductName = "Zupa pomidorowa", Price = 12M, ProductKind = ProductKind.Soup });

            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                        new { Id = Guid.NewGuid(), ProductName = "Zupa rosół", Price = 10M, ProductKind = ProductKind.Soup });
        }

        private static void AddMainDishWithAdditions(IDbConnection dbConnection)
        {
            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                            new { Id = Guid.NewGuid(), ProductName = "Schabowy z frytkami/ryżem/ziemniakami", Price = 30M, ProductKind = ProductKind.MainDish });

            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                        new { Id = Guid.NewGuid(), ProductName = "Ryba z frytkami", Price = 28M, ProductKind = ProductKind.MainDish });

            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                        new { Id = Guid.NewGuid(), ProductName = "Placek po węgiersku ", Price = 27M, ProductKind = ProductKind.MainDish });

            dbConnection.Execute("INSERT INTO additions (Id, AdditionName, Price, AdditionKind) VALUES (@Id, @AdditionName, @Price, @AdditionKind)",
                        new { Id = Guid.NewGuid(), AdditionName = "Bar sałatkowy", Price = 5M, AdditionKind = ProductKind.MainDish });
            dbConnection.Execute("INSERT INTO additions (Id, AdditionName, Price, AdditionKind) VALUES (@Id, @AdditionName, @Price, @AdditionKind)",
                        new { Id = Guid.NewGuid(), AdditionName = "Zestaw sosów", Price = 6M, AdditionKind = ProductKind.MainDish });
        }

        private static void AddPizzaWithAdditions(IDbConnection dbConnection)
        {
            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                            new { Id = Guid.NewGuid(), ProductName = "Pizza Margheritta", Price = 20M, ProductKind = ProductKind.Pizza });

            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                        new { Id = Guid.NewGuid(), ProductName = "Pizza Vegetariana", Price = 22M, ProductKind = ProductKind.Pizza });

            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                        new { Id = Guid.NewGuid(), ProductName = "Pizza Tosca", Price = 25M, ProductKind = ProductKind.Pizza });

            dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                        new { Id = Guid.NewGuid(), ProductName = "Pizza Venecia", Price = 25M, ProductKind = ProductKind.Pizza });

            dbConnection.Execute("INSERT INTO additions (Id, AdditionName, Price, AdditionKind) VALUES (@Id, @AdditionName, @Price, @AdditionKind)",
                        new { Id = Guid.NewGuid(), AdditionName = "Podwójny ser", Price = 2M, AdditionKind = ProductKind.Pizza });
            dbConnection.Execute("INSERT INTO additions (Id, AdditionName, Price, AdditionKind) VALUES (@Id, @AdditionName, @Price, @AdditionKind)",
                        new { Id = Guid.NewGuid(), AdditionName = "Salami", Price = 2M, AdditionKind = ProductKind.Pizza });
            dbConnection.Execute("INSERT INTO additions (Id, AdditionName, Price, AdditionKind) VALUES (@Id, @AdditionName, @Price, @AdditionKind)",
                        new { Id = Guid.NewGuid(), AdditionName = "Szynka", Price = 2M, AdditionKind = ProductKind.Pizza });
            dbConnection.Execute("INSERT INTO additions (Id, AdditionName, Price, AdditionKind) VALUES (@Id, @AdditionName, @Price, @AdditionKind)",
                        new { Id = Guid.NewGuid(), AdditionName = "Pieczarki", Price = 2M, AdditionKind = ProductKind.Pizza });
        }
    }
}
