using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Owin.Hosting;
using NLog;
using Owin;

using TrainingAPI;
using TrainingDataLayer.Training;


namespace TrainingIntegrationTests
{
    public class TestStartup : Startup
    {
        private readonly string _dbConnectionString;

        public TestStartup(string dbConnectionString)
        {
            this._dbConnectionString = dbConnectionString;
        }

        public override void RegisterDependencies(IAppBuilder app, ContainerBuilder builder)
        {
            base.RegisterDependencies(app, builder);
            builder.Register(x => LogManager.GetCurrentClassLogger()).As<ILogger>().InstancePerLifetimeScope();
            builder.Register(x => new TestTrainingSettings(this._dbConnectionString)).As<ITrainingSettings>().InstancePerLifetimeScope();
        }
    }
    public class WebServer:IDisposable
    {
        private readonly Task<IDisposable> _testServer;

        public WebServer(string baseAddress, string dbConnectionString)
        {
            this._testServer = this.CreateTestServer(baseAddress, dbConnectionString);
        }

        private async Task<IDisposable> CreateTestServer(string baseAddress, string dbConnectionString)
        {
            return  WebApp.Start(baseAddress,appBuilder => new TestStartup(dbConnectionString).Configuration(appBuilder));
        }
        public void Dispose()
        {
            try
            {
                this._testServer.Wait();
            }
            catch (AggregateException ex)
            {
                foreach (Exception e in ex.InnerExceptions)
                {
                    throw e;
                }
            }

            this._testServer.Dispose();
        }
    }
}
