using Domain;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace QueueConsumer;
//Batch related
internal class ConsumerService :  IConsumer<Batch<QueueMessage>>
{
    public Task Consume(ConsumeContext<Batch<QueueMessage>> context)
    {
        var message = "Batch started ";
        for (int i = 0; i < context.Message.Length; i++)
        {
            ConsumeContext<QueueMessage> audit = context.Message[i];
            message += " " + audit.Message.Value;
        }

        message += " Batch completed";

        Console.WriteLine(message);

        return Task.CompletedTask;
    }
}