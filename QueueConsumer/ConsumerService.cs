using Domain;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace QueueConsumer;

internal class ConsumerService :  IConsumer<QueueMessage>
{
    public Task Consume(ConsumeContext<QueueMessage> context)
    {
        Console.WriteLine(context.Message.Value);
        return Task.CompletedTask;
    }
}