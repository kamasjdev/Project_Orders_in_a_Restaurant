using Castle.Windsor;
using Restaurant.ApplicationLogic;
using Restaurant.Infrastructure;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;

namespace Restaurant.UI
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
