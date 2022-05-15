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
            container.Register(Component.For<IAdditonRepository>().ImplementedBy<AdditonRepository>().LifestyleScoped());
            container.Register(Component.For<IProductSaleRepository>().ImplementedBy<ProductSaleRepository>().LifestyleScoped());
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
                var createProductTable = @"BEGIN TRANSACTION;
                                           CREATE TABLE products (
                                                Id TEXT NOT NULL,
	                                            ProductName TEXT NOT NULL,
                                                Price REAL NOT NULL,
                                                ProductKind INTEGER NOT NULL,
                                                PRIMARY KEY (Id)
                                           );
                                           CREATE INDEX idx_products_product_name ON products (ProductName);
                                           CREATE INDEX idx_products_product_kind ON products (ProductKind);
                                           COMMIT;";

                var createOrderTable = @"BEGIN TRANSACTION;
                                         CREATE TABLE orders (
	                                            Id TEXT NOT NULL,
	                                            OrderNumber TEXT NOT NULL,
	                                            Created TEXT NOT NULL,
                                                Price REAL NOT NULL,
	                                            Email TEXT NOT NULL,
                                                Note TEXT,
                                                PRIMARY KEY (Id)
                                         );
                                         CREATE UNIQUE INDEX idx_orders_order_number ON orders (OrderNumber);
                                         CREATE INDEX idx_orders_created ON orders (Created);
                                         CREATE INDEX idx_orders_email ON orders (Email);
                                         COMMIT;";

                var createAdditionTable = @"BEGIN TRANSACTION;
                                            CREATE TABLE additions (
	                                            Id TEXT NOT NULL,
	                                            AdditionName TEXT NOT NULL,
                                                Price REAL NOT NULL,
                                                ProductKind INTEGER NOT NULL,
                                                PRIMARY KEY (Id)
                                            );
                                            CREATE INDEX idx_additions_addition_name ON additions (AdditionName);
                                            CREATE INDEX idx_additions_product_kind ON additions (ProductKind);
                                            COMMIT;";
                
                var createProductSaleTable = @"BEGIN TRANSACTION;
                                               CREATE TABLE product_sales (
	                                            Id TEXT NOT NULL,
	                                            ProductId TEXT NOT NULL,
                                                OrderId TEXT,
                                                AdditionId TEXT,
                                                EndPrice REAL NOT NULL,
	                                            Email TEXT NOT NULL,
                                                ProductSaleState INTEGER NOT NULL,
                                                CONSTRAINT FK_PRODUCTS FOREIGN KEY (ProductId) REFERENCES products,
                                                CONSTRAINT FK_ORDERS FOREIGN KEY (OrderId) REFERENCES orders,
                                                CONSTRAINT FK_ADDITIONS FOREIGN KEY (AdditionId) REFERENCES additions,
                                                PRIMARY KEY (Id)
                                            );
                                            CREATE INDEX idx_product_sales_product_id ON product_sales (ProductId);
                                            CREATE INDEX idx_product_sales_order_id ON product_sales (OrderId);
                                            CREATE INDEX idx_product_sales_addition_id ON product_sales (AdditionId);
                                            CREATE INDEX idx_product_sales_email ON product_sales (Email);
                                            CREATE INDEX idx_product_sales_product_sale_state ON product_sales (ProductSaleState);
                                            COMMIT;";
                
                var createMigrationTable = @"BEGIN TRANSACTION;
                                             CREATE TABLE migrations (
	                                            Id TEXT,
                                                Name TEXT,
                                                Version INTEGER NOT NULL,
                                                PRIMARY KEY (Id)
                                            );
                                            CREATE INDEX idx_migrations_name ON migrations (Name);
                                            COMMIT;";

                connection.Execute(createProductTable);
                connection.Execute(createOrderTable);
                connection.Execute(createAdditionTable);
                connection.Execute(createProductSaleTable);
                connection.Execute(createMigrationTable);
            }
        }
    }
}
