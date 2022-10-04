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
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Processors;
using System;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner;
using Restaurant.Migrations.Orders;
using FluentMigrator.Runner.Processors.SQLite;
using FluentMigrator;

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
            container.AddFluentMigrator(appSettings["RestaurantDb"]);

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

        private static IWindsorContainer AddFluentMigrator(this IWindsorContainer container, string connectionString)
        {
#pragma warning disable CS0612 // old style initialized New is initialized by ServiceProvider
            var announcer = new ConsoleAnnouncer()
            {
                ShowSql = true,
            };
           var options = new ProcessorOptions()
            {
                ConnectionString = connectionString,
                Timeout = TimeSpan.FromSeconds(5)
            };
            var processorFactory = new SQLiteProcessorFactory();
            var context = new RunnerContext(announcer)
            {
                AllowBreakingChange = true,
                Timeout = 5,
            };
            container.Register(Component.For<IMigrationProcessor>()
                        .UsingFactoryMethod(factory =>
                        {
                            return processorFactory.Create(connectionString, announcer, options);
                        }).LifestyleTransient());
            container.Register(Component.For<MigrationRunner>()
                        .UsingFactoryMethod(factory =>
                        {
                            var runner = new MigrationRunner(
                                typeof(InitCreateTableOrders_04_10_2022_18_05).Assembly,
                                context,
                                factory.Resolve<IMigrationProcessor>());
                            return runner;
                        }).LifestyleTransient());
#pragma warning restore CS0612 // old style initialized New is initialized by ServiceProvider

            return container;
        }

        public static void UseInfrastructure(this IWindsorContainer container)
        {
            using (var dispose = container.BeginScope())
            {
                var migrationRunner = container.Resolve<MigrationRunner>();
                migrationRunner.MigrateUp();
            }
        }
    }
}
