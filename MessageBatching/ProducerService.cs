using Domain;
using MassTransit;
using MassTransit.Monitoring.Performance;
using Microsoft.Extensions.Hosting;

namespace QueueProducer;

internal class ProducerService : IHostedService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private const int MessageCount = 100;
    public ProducerService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Thread.Sleep(TimeSpan.FromSeconds(20));
        for (var i = 0; i < MessageCount; i++)
            await _publishEndpoint.Publish(new QueueMessage
            {
                Value = "test" + i
            }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}