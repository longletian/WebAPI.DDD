using Dapr.Client;
using DomainBase;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace InfrastructureBase.EventBus
{
    public class DaprEventBus : IEventBus
    {
        private const string PubSubName = "pubsub";
        private readonly DaprClient daprClient;
        private readonly ILogger<DaprEventBus> logger;

        public DaprEventBus(DaprClient daprClient, ILogger<DaprEventBus> logger)
        {
            this.daprClient = daprClient ?? throw new ArgumentNullException(nameof(DaprClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public  void PublishAsync<TIntegrationEvent>(TIntegrationEvent @event, string exchangeName) where TIntegrationEvent : Event
        {
            var topicName = @event.GetType().Name;
            daprClient.PublishEventAsync<object>(PubSubName, topicName, @event);
        }

        public void Subscribe<TH, T>(string exchangeName, string subscriberName)
            where TH : IEventHandle<T>
            where T : Event
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T, TH>()
            where T : Event
            where TH : IEventHandle<T>
        {
            throw new NotImplementedException();
        }
    }
}
