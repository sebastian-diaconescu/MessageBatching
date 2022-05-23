using Domain;
using MassTransit;
using MassTransit.Caching;
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
        await _publishEndpoint.Publish<QueueMessage>(new QueueMessage()
        {
            Value = "test"
        }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}