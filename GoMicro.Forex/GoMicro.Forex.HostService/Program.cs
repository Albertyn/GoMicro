using System;
using Topshelf;
using Autofac;
using GoMicro.Forex.WebApi;
using GoMicro.Forex.DI;
using GoMicro.Forex.Components.Repositories;
using GoMicro.Forex.Core;

namespace GoMicro.Forex.HostService
{
    class Program
    {
        static int Main(string[] args)
        {
            // config values

            var serviceName = "GoMicroForex";
            var serviceDescription = "Exchange rate micro-service";
            var serviceDisplayName = "GoMicro Forex Service";
            var serviceInstanceName = "GMFX01";

            string conn = "mongodb://localhost:27017/ExchangeRates";

            IoC.BootstrapContainer(builder => {

                builder.RegisterInstance(new MongoConfiguration(conn))
                .As<IMongoConfiguration>();

                IApiSettings apiSettings = new ApiSettings()
                {
                    AskTimeOut = 10000,
                    Url = @"http://localhost:9091"
                };
                builder.RegisterInstance(apiSettings).As<IApiSettings>().SingleInstance();

                IFixerSettings fixerSettings = new FixerSettings()
                {
                    BaseUrl = "http://api.fixer.io/",
                    Currencies = "EUR,USD,JPY,BGN,CZK,DKK,GBP,HUF,PLN,RON,SEK,CHF,NOK,HRK,RUB,TRY,AUD,BRL,CAD,CNY,HKD,IDR,ILS,INR,KRW,MXN,MYR,NZD,PHP,SGD,THB,ZAR"
                };
                builder.RegisterInstance(fixerSettings).As<IFixerSettings>().SingleInstance();

            });

                // run topshelf service

                return (int)HostFactory.Run(serviceConfig =>
            {
                serviceConfig.SetServiceName(serviceName);
                serviceConfig.SetDescription(serviceDescription);
                serviceConfig.SetDisplayName(serviceDisplayName);
                serviceConfig.SetInstanceName(serviceInstanceName);

                serviceConfig.UseAssemblyInfoForServiceInfo();
                serviceConfig.RunAsPrompt();
                serviceConfig.StartAutomatically();
                serviceConfig.UseNLog();


                serviceConfig.Service<WebApiHostService>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(n => new WebApiHostService());
                    serviceInstance.WhenStarted((s, hostControl) => s.Start(hostControl));
                    serviceInstance.WhenStopped((s, hostControl) => s.Stop(hostControl));
                    //
                    serviceInstance.WhenCustomCommandReceived((s, hostControl, cmd) => s.ExecuteCustomCommand(cmd));
                });

                serviceConfig.SetStartTimeout(TimeSpan.FromSeconds(10));
                serviceConfig.SetStopTimeout(TimeSpan.FromSeconds(10));

                serviceConfig.EnableServiceRecovery(recover => {
                    recover.SetResetPeriod(2);
                    recover.RestartService(2);
                    recover.OnCrashOnly();
                });

            });
        }
    }
}
