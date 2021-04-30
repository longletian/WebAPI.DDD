using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainBase.Event
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
