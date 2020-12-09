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
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Timeout");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostBuilder, services) =>
                {
                    // https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1#shutdowntimeout
                    services.Configure<HostOptions>(option =>
                    {
                        var timeout = int.Parse(Environment.GetEnvironmentVariable("TIMEOUT") ?? "60");
                        Console.WriteLine($"Timeout: {timeout}");
                        option.ShutdownTimeout = TimeSpan.FromSeconds(timeout);
                    });
                    services.AddHostedService<Worker>();
                });
    }
}
