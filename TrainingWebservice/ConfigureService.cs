using Topshelf;


namespace TrainingWebservice
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<MyService>(service =>
                {
                    service.ConstructUsing(x => new MyService());
                    service.WhenStarted(x => x.BackUpTidRollOver());
                });
            
                //Setup Account that window service use to run.  
                configure.RunAsLocalSystem();
                configure.SetServiceName("TidRolloverSchedulingWebserviceDemo");
                configure.SetDisplayName("TidRolloverSchedulingWebserviceDemo");
                configure.SetDescription("Tid Rollover Scheduling Web service Demo");
            });
        }
    }
}
