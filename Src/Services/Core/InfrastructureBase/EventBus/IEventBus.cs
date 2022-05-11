﻿using DomainBase;
using System.Threading.Tasks;

namespace InfrastructureBase
{
    public interface IEventBus
    {
        void PublishAsync<TIntegrationEvent>(TIntegrationEvent @event, string exchangeName)
           where TIntegrationEvent : Event;

        void Subscribe<TH, T>(string exchangeName, string subscriberName)
           where TH : IEventHandle<T>
           where T : Event;


        void Unsubscribe<T, TH>()
            where TH : IEventHandle<T>
            where T : Event;


        //void SubscribeDynamic<TH>(string eventName)
        //     where TH : IEventHandle;

        //void UnsubscribeDynamic<TH>(string eventName)
        //    where TH : IEventHandle;
    }
}
