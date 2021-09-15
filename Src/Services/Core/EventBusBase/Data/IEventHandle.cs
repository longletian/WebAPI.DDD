using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DomainBase.Event
{

    //IEventHandler 定义了事件处理方法
    public interface IEventHandler 
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> HandleAsync(Event @event, CancellationToken cancellationToken = default(CancellationToken));
    }

    /// <summary>
    /// 事件处理
    /// </summary>
    public interface IEventHandler<in T> : INotificationHandler<T>, IEventHandler where T : Event
    {
       
    }
}
