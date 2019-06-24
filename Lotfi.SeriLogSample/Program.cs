using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks;
using System;

namespace Lotfi.SeriLogSample
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {

            RegisterServices();

            var service = _serviceProvider.GetService<IService>();
            service.TestLog();

            Console.WriteLine("Hello World!");
            Console.WriteLine("------------");
            Console.WriteLine("------------");
            Console.WriteLine("------------");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            using (var log = new LoggerConfiguration()
                .WriteTo.Console(restrictedToMinimumLevel:Serilog.Events.LogEventLevel.Information)
                .WriteTo.File("log/log.txt",Serilog.Events.LogEventLevel.Error)
                .CreateLogger())
            {
                log.Information("Hello from Information Serilog");
                log.Warning("Hello from Warning Serilog");
                log.Error("Hello from Error Serilog");
                log.Fatal("Hello from Fatal Serilog");
                
            }
            

        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();

            collection.AddSingleton<IService, ServiceTest>();
            

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .WriteTo.File("log/log.txt", Serilog.Events.LogEventLevel.Error)
                .CreateLogger();
            collection.AddSingleton<ILogger>(Log.Logger);


            _serviceProvider = collection.BuildServiceProvider();

        }
    }
}
