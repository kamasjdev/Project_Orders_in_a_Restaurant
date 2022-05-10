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
using Restaurant.Infrastructure.Requests;
using Restaurant.Infrastructure.Migrations;
using Restaurant.Infrastructure.Mappings;
using Restaurant.ApplicationLogic.Interfaces;
using Restaurant.Infrastructure.DAL;

namespace Restaurant.Infrastructure
{
    public static class Extensions
    {
        public static IWindsorContainer AddInfrastructure(this IWindsorContainer container, NameValueCollection appSettings)
        {
            container.Register(Component.For<IOrderRepository>().ImplementedBy<OrderRepository>().LifestyleScoped());
            container.Register(Component.For<IProductRepository>().ImplementedBy<ProductRepository>().LifestyleScoped());
            container.AddDbConnection(appSettings);
            container.Register(Component.For<IRequestHandler>()
                        .UsingFactoryMethod(factory =>
                        {
                            return new RequestHandler(container);
                        }).LifestyleSingleton());
            container.ApplyMappings();
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifestyleScoped());
            return container;
        }
        
        public static IWindsorContainer AddDbConnection(this IWindsorContainer container, NameValueCollection appSettings)
        {
            var connectionString = appSettings["RestaurantDb"];
            container.Register(Component.For<IDbConnection>()
                        .UsingFactoryMethod(kernel => {
                                var connection = new SQLiteConnection(connectionString);
                                connection.Open();
                                return connection;
                            })
                        .LifestyleScoped());
            return container;
        }

        public static void UseInfrastructure(this IWindsorContainer container)
        {
            using (var dispose = container.BeginScope())
            {
                var connection = container.Resolve<IDbConnection>();
                EnsureTablesAreCreated(connection);
                SeedData.AddData(connection);
            }
        }

        private static void EnsureTablesAreCreated(IDbConnection connection)
        {
            var result = connection.Query<string>("SELECT name FROM sqlite_master WHERE type=@Type AND name IN (@t1, @t2, @t3)", new { Type = "table", t1 = "products", t2 = "orders", t3 = "order_product" }).ToList();

            if (!result.Any())
            {
                var createProductTable = @"CREATE TABLE products (
                                                Id TEXT NOT NULL,
	                                            ProductName TEXT NOT NULL,
                                                Price REAL NOT NULL,
                                                PRIMARY KEY (Id)
                                            );";
                var createOrderTable = @"CREATE TABLE orders (
	                                            Id TEXT NOT NULL,
	                                            OrderNumber TEXT NOT NULL,
	                                            Created TEXT NOT NULL,
                                                Price REAL NOT NULL,
	                                            Email TEXT NOT NULL,
                                                PRIMARY KEY (Id)
                                            );";
                
                var createOrderProductTable = @"CREATE TABLE order_product (
	                                            ProductId TEXT,
                                                OrderId TEXT,
                                                CONSTRAINT FK_PRODUCTS FOREIGN KEY (ProductId) REFERENCES products,
                                                CONSTRAINT FK_ORDERS FOREIGN KEY (OrderId) REFERENCES orders
                                            );";
                
                var createMigrationTable = @"CREATE TABLE migrations (
	                                            Id TEXT,
                                                Name TEXT,
                                                Version INTEGER NOT NULL
                                            );";

                connection.Execute(createProductTable);
                connection.Execute(createOrderTable);
                connection.Execute(createOrderProductTable);
                connection.Execute(createMigrationTable);
            }
        }
    }
}
