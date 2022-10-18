using System.Threading;
using System.Threading.Tasks;
using Elsa.Scripting.Liquid.Messages;
using MediatR;

namespace Workflow.Infrastructure.Data.Handlers
{
    public class LiquidConfigurationHandler : INotificationHandler<EvaluatingLiquidExpression>
    {
        public Task Handle(EvaluatingLiquidExpression notification, CancellationToken cancellationToken)
        {
            var context = notification.TemplateContext;
            // context.MemberAccessStrategy.Register<User>();
            // context.MemberAccessStrategy.Register<RegistrationModel>();
            
            return Task.CompletedTask;
        }
    }
}