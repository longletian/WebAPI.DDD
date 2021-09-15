using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusBase.Data
{
    /// <summary>
    ///  为了实现cqrs IRequest 单播
    /// </summary>
    public interface ICommand:IRequest
    {

    }

    /// <summary>
    /// 具有返回值
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommand<out TResponse> : IRequest<TResponse>
    { 
    
    }
}
