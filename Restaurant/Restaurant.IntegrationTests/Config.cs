using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using NUnit.Framework;
using Restaurant.IntegrationTests.Common;
using System;
using System.IO;

namespace Restaurant.IntegrationTests
{
    [SetUpFixture]
    public class Config
    {
        public static IWindsorContainer Container;
        private static IDisposable dispose;

        [OneTimeSetUp]
        public void OnetTimeSetup()
        {
            Container = new TestApplicationFactory().StartApplication();
            dispose = Container.BeginScope();
        }

        [OneTimeTearDown]
        public void OnetTimeTeardown()
        {
            System.Data.SQLite.SQLiteConnection.ClearAllPools();
            if (dispose != null)
                dispose.Dispose();
            if (Container != null)
                Container.Dispose();
            File.Delete(Environment.CurrentDirectory + Path.DirectorySeparatorChar + TestApplicationFactory.DB_FILE_NAME);
        }
    }
}