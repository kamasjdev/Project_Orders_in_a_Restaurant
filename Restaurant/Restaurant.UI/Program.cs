using Castle.Windsor;
using Restaurant.ApplicationLogic;
using Restaurant.Infrastructure;
using System;
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
            container.AddInfrastructure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
