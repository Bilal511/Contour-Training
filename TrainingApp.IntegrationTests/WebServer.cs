using Autofac;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Logging;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingDataLayer.Training;

namespace TrainingApp.IntegrationTests
{
    public class TestStartup : TrainingAPI.Startup
    {
        private readonly string DBConnectionString;

        public TestStartup(string DBConnectionString)
        {
            this.DBConnectionString = DBConnectionString;
        }

        public override void RegisterDependencies(IAppBuilder app, ContainerBuilder builder)
        {
            base.RegisterDependencies(app, builder);
            builder.Register(x => NLog.LogManager.GetCurrentClassLogger()).As<ILogger>().InstancePerLifetimeScope();
            builder.Register(x => new TestTrainingSettings(this.DBConnectionString)).As<ITrainingSettings>().SingleInstance();
        }


    }

    public class WebServer : IDisposable
    {
        private readonly Task<IDisposable> testServer;

        public WebServer(string baseAddress, string DbConnectionString)
        {
            this.testServer = this.CreateTestServer(baseAddress, DbConnectionString);
        }

        private Task<IDisposable> CreateTestServer(string baseAddress, string DbConnectionString)
        {
            return Task.FromResult(WebApp.Start(baseAddress, appBuilder => new TestStartup(DbConnectionString).Configuration(appBuilder)));
        }

        public void Dispose()
        {
            try
            {
                this.testServer.Wait();
            }
            catch (AggregateException ex)
            {
                foreach (Exception e in ex.InnerExceptions)
                {
                    throw e;
                }
            }
            this.testServer.Dispose();
        }
    }
}
