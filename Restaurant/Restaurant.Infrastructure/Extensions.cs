using Castle.Windsor;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Repositories;
using Castle.MicroKernel.Registration;
using System.Collections.Specialized;
using System.Data;
using Castle.MicroKernel.Lifestyle;
using System.Data.SQLite;
using Dapper;
using System.Linq;

namespace Restaurant.Infrastructure
{
    public static class Extensions
    {
        public static IWindsorContainer AddInfrastructure(this IWindsorContainer container, NameValueCollection appSettings)
        {
            container.Register(Component.For<IOrderRepository>().ImplementedBy<OrderRepository>().LifestyleScoped());
            container.Register(Component.For<IProductRepository>().ImplementedBy<ProductRepository>().LifestyleScoped());
            container.AddDbConnection(appSettings);
            return container;
        }
        
        public static IWindsorContainer AddDbConnection(this IWindsorContainer container, NameValueCollection appSettings)
        {
            var connectionString = appSettings["RestaurantDb"];
            container.Register(Component.For<IDbConnection>()
                        .UsingFactoryMethod(kernel => new SQLiteConnection(connectionString))
                        .LifestyleScoped());
            return container;
        }

        public static void UseInfrastructure(this IWindsorContainer container)
        {
            using (var dispose = container.BeginScope())
            {
                var connection = container.Resolve<IDbConnection>();
                EnsureTablesAreCreated(connection);
            }
        }

        private static void EnsureTablesAreCreated(IDbConnection connection)
        {
            connection.Open();
            var result = connection.Query<string>("SELECT name FROM sqlite_master WHERE type=@Type AND name IN (@t1, @t2)", new { Type = "table", t1 = "products", t2 = "orders" }).ToList();

            if (!result.Any())
            {
                var createProductTable = @"CREATE TABLE products (
                                                Id TEXT NOT NULL,
	                                            ProductName TEXT NOT NULL,
	                                            Created TEXT NOT NULL,
                                                WholePart REAL NOT NULL,
                                                FractionalPart REAL NOT NULL,
                                                PRIMARY KEY (Id)
                                            );";
                var createOrderTable = @"CREATE TABLE orders (
	                                            Id TEXT NOT NULL,
	                                            ProductId TEXT NOT NULL,
	                                            OrderNumber TEXT NOT NULL,
	                                            Created TEXT NOT NULL,
                                                WholePart REAL NOT NULL,
                                                FractionalPart REAL NOT NULL,
	                                            Email TEXT NOT NULL,
                                                CONSTRAINT FK_PRODUCTS FOREIGN KEY (ProductId) REFERENCES products,
                                                PRIMARY KEY (Id)
                                            );";

                connection.Execute(createProductTable);
                connection.Execute(createOrderTable);
            }
        }
    }
}
