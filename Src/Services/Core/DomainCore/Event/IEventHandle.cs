﻿using System.Threading.Tasks;
namespace DomainBase
{
    /// <summary>
    /// 事件处理
    /// </summary>
    public interface IEventHandle
    {

    }

    public interface IEventHandle<in TIntegrationEvent> : IEventHandle where TIntegrationEvent : Event
    {
        Task Handle(TIntegrationEvent @event);
    }
}
