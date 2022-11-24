using System;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using ContourApiAuthentication;
using TrainingBusinessLayer;
using TrainingDataLayer.Training;
using System.Reflection;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(TrainingAPI.Startup))]
namespace TrainingAPI
{
    public class Startup
    {
        public virtual void RegisterDependencies(IAppBuilder app, ContainerBuilder builder)
        {
            builder.Register(x => new AuthApiInitialize()).As<IAuthApiInitialise>().SingleInstance();
            builder.RegisterType<AuthorizationServerProvider>().UsingConstructor(typeof(IAuthApiInitialise)).As<AuthorizationServerProvider>().SingleInstance();

            builder.Register(x => new TrainingSettings()).As<ITrainingSettings>().SingleInstance();
            builder.RegisterType<TrainingBusinessLayer.TrainingBL>().As<TrainingBusinessLayer.TrainingBL>().SingleInstance();
            builder.RegisterType<TrainingDataLayer.TrainingDataLayer>().As<TrainingDataLayer.TrainingDataLayer>().SingleInstance();
        }


        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            this.RegisterDependencies(app, builder);
            builder.RegisterWebApiFilterProvider(config);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
           // config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var authApiInitialize = container.Resolve<IAuthApiInitialise>();
            var settings = container.Resolve<ITrainingSettings>();
            authApiInitialize.InitialiseAuthTable(settings.GetDbConnectionString());

            var authProvider = container.Resolve<AuthorizationServerProvider>();

            var options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                Provider = authProvider
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseAutofacWebApi(config);

            app.UseWebApi(config);
        }


    }
}