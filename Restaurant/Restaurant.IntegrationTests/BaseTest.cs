using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using NUnit.Framework;
using Restaurant.IntegrationTests.Common;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Restaurant.IntegrationTests
{
    public class BaseTest
    {
        protected IWindsorContainer container;
        private IDisposable dispose;

        [OneTimeSetUp]
        public void OnetTimeSetup()
        {
            container = new TestApplicationFactory().StartApplication();
            dispose = container.BeginScope();
        }

        [OneTimeTearDown]
        public void OnetTimeTeardown()
        {
            System.Data.SQLite.SQLiteConnection.ClearAllPools();
            if (dispose != null)
                dispose.Dispose();
            if (container != null)
                container.Dispose();
            File.Delete(Environment.CurrentDirectory + Path.DirectorySeparatorChar + TestApplicationFactory.DB_FILE_NAME);
        }
    }
}