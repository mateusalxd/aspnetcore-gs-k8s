using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace worker
{
    internal class Worker : BackgroundService
    {
        private bool _isProcessing = false;
        private bool _isConnected = false;

        public Worker(IHostApplicationLifetime appLifetime)
        {
            // https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1#ihostapplicationlifetime
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var buffer = new StringBuilder();
                var random = new Random();
                var creationTime = int.Parse(Environment.GetEnvironmentVariable("CREATION_TIME") ?? "20");
                Console.WriteLine($"Creation time: {creationTime}");
                _isConnected = true;

                while (true)
                {
                    Console.WriteLine($"ExecuteAsync started");
                    stoppingToken.ThrowIfCancellationRequested();

                    _isProcessing = true;
                    for (int i = 0; i < creationTime; i++)
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
                Console.WriteLine("Cancellation Requested on ExecuteAsync");
            }
        }

        private void OnStopping()
        {
            Console.WriteLine("OnStopping has been called.");
            _isConnected = false;
        }

        private void OnStopped()
        {
            Console.WriteLine("OnStopped has been called.");
            Console.WriteLine($"_isProcessing = {_isProcessing}; _isConnected = {_isConnected}");
            Console.WriteLine($"Graceful shutdown");
        }
    }

}