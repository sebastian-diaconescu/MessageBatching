using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace QueueConsumer
{
    class ConsumerDeffintion:
        ConsumerDefinition<ConsumerService>
    {
        private const int PrefetchCount = 10;

        public ConsumerDeffintion()
        {
            // override the default endpoint name
            EndpointName = "order-service";

            //this is batching related prefetch count
            Endpoint(x => x.PrefetchCount = PrefetchCount);

            // limit the number of messages consumed concurrently
            // this applies to the consumer only, not the endpoint
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ConsumerService> consumerConfigurator)
        {
            // configure message retry with millisecond intervals
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));

            // use the outbox to prevent duplicate events from being published
            endpointConfigurator.UseInMemoryOutbox();

            //this is batching related prefetch count
            consumerConfigurator.Options<BatchOptions>(options => options
                .SetMessageLimit(PrefetchCount)
                .SetTimeLimit(10000)
                //number of batches that can be executed concurently
                .SetConcurrencyLimit(1));
        }
    }
}
