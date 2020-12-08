using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostBuilder, services) =>
                {
                    // https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1#shutdowntimeout
                    services.Configure<HostOptions>(option =>
                    {
                        option.ShutdownTimeout = TimeSpan.FromSeconds(60);
                    });
                    services.AddHostedService<Worker>();
                });
    }
}
