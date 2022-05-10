using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using Dapper;
using Restaurant.Domain.Entities;
using System;
using System.Data;

namespace Restaurant.IntegrationTests.Common
{
    internal class DataSeed
    {
        public static void AddData(IWindsorContainer windsorContainer)
        {
            using (var scope = windsorContainer.BeginScope())
            {
                var dbConnection = windsorContainer.Resolve<IDbConnection>();

                dbConnection.Execute("INSERT INTO products (Id, ProductName, Price) VALUES(@Id, @ProductName, @Price)",
                    new
                    {
                        Id = new Guid("6f542d82-4f0d-4bd6-b90b-6d2b7b79efdd"),
                        ProductName = "Product A #1",
                        Price = 500M
                    });

                dbConnection.Execute("INSERT INTO products (Id, ProductName, Price) VALUES(@Id, @ProductName, @Price)",
                    new
                    {
                        Id = new Guid("b9302f85-9fcc-446f-9dd3-f8510fc864b9"),
                        ProductName = "Product B #1",
                        Price = 500M
                    });

                dbConnection.Execute("INSERT INTO orders (Id, Created, Email, OrderNumber, Price) VALUES(@Id, @Created, @Email, @OrderNumber, @Price)",
                    new
                    {
                        Id = new Guid("6f542d82-4f0d-4bd6-b90b-6d2b7b79efdd"),
                        Created = DateTime.UtcNow,
                        Email = "email@email.com",
                        OrderNumber = "ORD/1",
                        Price = 1000M
                    });

                dbConnection.Execute("INSERT INTO orders (Id, Created, Email, OrderNumber, Price) VALUES(@Id, @Created, @Email, @OrderNumber, @Price)",
                    new
                    {
                        Id = new Guid("b9302f85-9fcc-446f-9dd3-f8510fc864b9"),
                        Created = DateTime.UtcNow,
                        Email = "email@email.com",
                        OrderNumber = "ORD/2",
                        Price = 2000M
                    });

                dbConnection.Execute(@"INSERT INTO order_product (ProductId, OrderId) 
                                   VALUES(@ProductId, @OrderId)",
                                       new { ProductId = new Guid("6f542d82-4f0d-4bd6-b90b-6d2b7b79efdd"), OrderId = new Guid("6f542d82-4f0d-4bd6-b90b-6d2b7b79efdd") });
                dbConnection.Execute(@"INSERT INTO order_product (ProductId, OrderId) 
                                   VALUES(@ProductId, @OrderId)",
                                       new { ProductId = new Guid("6f542d82-4f0d-4bd6-b90b-6d2b7b79efdd"), OrderId = new Guid("6f542d82-4f0d-4bd6-b90b-6d2b7b79efdd") });

                dbConnection.Execute(@"INSERT INTO order_product (ProductId, OrderId) 
                                   VALUES(@ProductId, @OrderId)",
                                       new { ProductId = new Guid("6f542d82-4f0d-4bd6-b90b-6d2b7b79efdd"), OrderId = new Guid("b9302f85-9fcc-446f-9dd3-f8510fc864b9") });
                dbConnection.Execute(@"INSERT INTO order_product (ProductId, OrderId) 
                                   VALUES(@ProductId, @OrderId)",
                                       new { ProductId = new Guid("6f542d82-4f0d-4bd6-b90b-6d2b7b79efdd"), OrderId = new Guid("b9302f85-9fcc-446f-9dd3-f8510fc864b9") });
                dbConnection.Execute(@"INSERT INTO order_product (ProductId, OrderId) 
                                   VALUES(@ProductId, @OrderId)",
                                       new { ProductId = new Guid("b9302f85-9fcc-446f-9dd3-f8510fc864b9"), OrderId = new Guid("b9302f85-9fcc-446f-9dd3-f8510fc864b9") });
                dbConnection.Execute(@"INSERT INTO order_product (ProductId, OrderId) 
                                   VALUES(@ProductId, @OrderId)",
                                       new { ProductId = new Guid("b9302f85-9fcc-446f-9dd3-f8510fc864b9"), OrderId = new Guid("b9302f85-9fcc-446f-9dd3-f8510fc864b9") });
            }
        }
    }
}
