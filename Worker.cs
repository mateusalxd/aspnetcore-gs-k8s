using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace worker
{
    internal class Worker : BackgroundService
    {
        private bool _isProcessing = false;

        public Worker(ILogger<Worker> logger, IHostApplicationLifetime appLifetime)
        {
            // https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1#ihostapplicationlifetime
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var buffer = new StringBuilder();
                var random = new Random();
                var length = 20;

                while (true)
                {
                    stoppingToken.ThrowIfCancellationRequested();

                    _isProcessing = true;
                    for (int i = 0; i < length; i++)
                    {
                        buffer.Append(Convert.ToChar(random.Next(32, 126)));
                        await Task.Delay(1000);
                    }

                    var filename = $"{DateTime.Now:ddMMyyyyHHmmss}.txt";
                    await File.WriteAllTextAsync(filename, buffer.ToString(), Encoding.UTF8);
                    Console.WriteLine($"{filename} was created.");
                    buffer.Clear();
                    _isProcessing = false;

                    await Task.Delay(2000);
                }
            }
            catch (OperationCanceledException)
            {
                System.Console.WriteLine("Cancellation Requested on ExecuteAsync");
            }
        }

        private void OnStarted()
        {
            Console.WriteLine("OnStarted has been called.");
        }

        private async void OnStopping()
        {
            Console.WriteLine("OnStopping has been called.");
            Console.WriteLine("Trying to finish processing.");
            while (_isProcessing)
            {
                await Task.Delay(1000);
            }
            Console.WriteLine("Bye guy.");
        }

        private void OnStopped()
        {
            Console.WriteLine("OnStopped has been called.");
        }
    }

}