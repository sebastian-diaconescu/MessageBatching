using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QueueConsumer
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("rabbit1", "/", h =>
                            {
                                h.Username("admin");
                                h.Password("admin");
                            });

                            cfg.ConfigureEndpoints(context);
                        });

                        x.AddConsumer<ConsumerService, ConsumerDeffintion>();
                    });

                    services.AddOptions<MassTransitHostOptions>()
                        .Configure(options =>
                        {
                            // if specified, waits until the bus is started before
                            // returning from IHostedService.StartAsync
                            // default is false
                            options.WaitUntilStarted = true;

                            // if specified, limits the wait time when starting the bus
                            options.StartTimeout = TimeSpan.FromSeconds(10);

                            // if specified, limits the wait time when stopping the bus
                            options.StopTimeout = TimeSpan.FromSeconds(30);
                        });

                });
            var hostBuild = builder
                .UseConsoleLifetime()
                .Build();

            await hostBuild.RunAsync();
        }
    }
}