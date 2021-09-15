using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusBase.Data
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
          where TCommand : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, TResult> :
        IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
