using Castle.Windsor;
using Restaurant.ApplicationLogic;
using Restaurant.Infrastructure;
using System.Collections.Specialized;
using System.Configuration;

namespace Restaurant.UI
{
    public class SetupApplication
    {
        public static IWindsorContainer Create()
        {
            var container = new WindsorContainer();
            container.AddApplicationLogic();
            var connectionStrings = new NameValueCollection();

            foreach (ConnectionStringSettings connectionStringSettings in ConfigurationManager.ConnectionStrings)
            {
                connectionStrings.Add(connectionStringSettings.Name, connectionStringSettings.ConnectionString);
            }

            container.AddInfrastructure(connectionStrings);
            container.UseInfrastructure();

            return container;
        }
    }
}
