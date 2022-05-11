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
                dbConnection.Execute("INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)",
                            new { Id = Guid.NewGuid(), ProductName = "Pizza", Price = 100.50M, ProductKind = ProductKind.Pizza });
                dbConnection.Execute("INSERT INTO migrations (Id, Name, Version) VALUES (@Id, @Name, @Version)",
                            new { Id = Guid.NewGuid(), Name = $"First_Migration_{DateTime.UtcNow.ToString("s", CultureInfo.GetCultureInfo("en-US"))}", Version = result?.Version + 1 ?? 1 });
            }
        }
    }
}
