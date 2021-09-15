using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainBase.Entitys
{

    // 事件总线实现发布/订阅功能，首先定义IEvent/IEventHandler，
    // 事件模型
    // INotification 和IRequest的区别 
    // 对于继承于 IRequest 接口的类来说，一个请求（request）只会有一个针对这个请求的处理程序（requestHandler），它可以返回值或者不返回任何信息；
    // 对于继承于 INotification 接口的类来说，一个通知（notification）会对应多个针对这个通知的处理程序（notificationHandlers），而它们不会返回任何的数据。
    public interface IEvent : INotification
    {
        Guid Id { get; }

        DateTime CreateDateTime { get; }
    }
}
