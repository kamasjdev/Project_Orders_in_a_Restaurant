using Castle.Windsor;
using Restaurant.ApplicationLogic;
using Restaurant.Infrastructure;
using Restaurant.Infrastructure.Requests;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
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
            container.Register(Castle.MicroKernel.Registration.Component.For<Form1>());
            container.Register(Castle.MicroKernel.Registration.Component.For<Menu>());
            container.Register(Castle.MicroKernel.Registration.Component.For<Settings>());
            container.Register(Castle.MicroKernel.Registration.Component.For<History>());
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
            Application.Run(container.Resolve<Form1>());
            //Application.Run(new Form1(container.Resolve<IRequestHandler>()));
        }
    }
}
