using Dapr.Client;
using DomainBase;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace InfrastructureBase.EventBus
{
    public class DaprEventBus : IEventBus,ITransientDependency
    {
        private const string PubSubName = "pubsub";
        private readonly DaprClient daprClient;
        private readonly ILogger<DaprEventBus> logger;

        public DaprEventBus(DaprClient daprClient, ILogger<DaprEventBus> logger)
        {
            this.daprClient = daprClient ?? throw new ArgumentNullException(nameof(DaprClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : Event
        {
            var topicName = @event.GetType().Name;
            await daprClient.PublishEventAsync<object>(PubSubName, topicName, @event);
        }
    }
}
