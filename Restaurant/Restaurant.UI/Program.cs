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
            var container = SetupApplication.Create();
            container.Register(Castle.MicroKernel.Registration.Component.For<MainPanel>().LifestyleSingleton());
            container.Register(Castle.MicroKernel.Registration.Component.For<Menu>().LifestyleSingleton());
            container.Register(Castle.MicroKernel.Registration.Component.For<Settings>().LifestyleSingleton());
            container.Register(Castle.MicroKernel.Registration.Component.For<History>().LifestyleSingleton());
            container.Register(Castle.MicroKernel.Registration.Component.For<Options>().LifestyleSingleton());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainPanel>());
        }
    }
}
