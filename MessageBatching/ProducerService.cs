using Domain;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace QueueProducer;

internal class ProducerService : IHostedService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ProducerService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Thread.Sleep(TimeSpan.FromSeconds(20));
        for (var i = 0; i < 10; i++)
            await _publishEndpoint.Publish(new QueueMessage
            {
                Value = "test"
            }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}