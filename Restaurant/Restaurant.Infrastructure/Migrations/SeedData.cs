using Dapper;
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
            dbConnection.Open();
            var result = dbConnection.Query<Migration>("SELECT Id, Name, Version FROM migrations WHERE 1=1 ORDER BY Version DESC LIMIT 1").SingleOrDefault();

            if (result is null)
            {
                dbConnection.Execute("INSERT INTO products (Id, ProductName, Price) VALUES (@Id, @ProductName, @Price)",
                            new { Id = Guid.NewGuid(), ProductName = "Pizza", Price = 100.50M });
                dbConnection.Execute("INSERT INTO migrations (Id, Name, Version) VALUES (@Id, @Name, @Version)",
                            new { Id = Guid.NewGuid(), Name = $"First_Migration_{DateTime.UtcNow.ToString("s", CultureInfo.GetCultureInfo("en-US"))}", Version = result?.Version + 1 ?? 1 });
            }

            dbConnection.Close();
        }
    }
}
