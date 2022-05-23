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
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.ConfigureEndpoints(context);
                        });

                        x.AddConsumer<ConsumerService, ConsumerDeffintion>();


                    });

                    

                });
            var hostBuild = builder
                .UseConsoleLifetime()
                .Build();

            await hostBuild.RunAsync();
        }
    }
}