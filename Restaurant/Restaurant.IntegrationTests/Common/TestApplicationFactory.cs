using Castle.Windsor;
using Restaurant.ApplicationLogic;
using Restaurant.Infrastructure;
using System.Collections.Specialized;

namespace Restaurant.IntegrationTests.Common
{
    internal class TestApplicationFactory
    {
        public static string DB_FILE_NAME = "restaurant_test.db";

        public IWindsorContainer StartApplication()
        {
            var container = new WindsorContainer();
            container.AddApplicationLogic();
            var connectionStrings = new NameValueCollection();
            connectionStrings.Add("RestaurantDb", $"Data Source={DB_FILE_NAME};New=True;BinaryGuid=False");
            container.AddInfrastructure(connectionStrings);
            container.UseInfrastructure();
            DataSeed.AddData(container);
            return container;
        }
    }
}
