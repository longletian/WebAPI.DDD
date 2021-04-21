﻿using Dapr.Client;
using DomainBase.Event;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureBase.EventBus
{
    public class EventBus : IEventBus
    {
        private const string PubSubName = "pubsub";
        private readonly DaprClient daprClient;
        private readonly ILogger<EventBus> logger;

        public EventBus(DaprClient daprClient, ILogger<EventBus> logger)
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
