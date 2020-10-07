using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
                .WriteTo.File("./logs/log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, outputTemplate: "[{Level:u4} {Timestamp:HH:mm:ss} ] {Message:lj}{NewLine}")
                .WriteTo.Console(outputTemplate: "[{Level:u4} {Timestamp:HH:mm:ss} ] {Message:lj}{NewLine}")
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();
            Log.Information("Host created.");
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
