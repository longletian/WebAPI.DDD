using System.Threading;
using System.Threading.Tasks;
using Elsa.Scripting.Liquid.Messages;
using MediatR;
using Fluid;
using Workflow.Api.Models.Dtos;
using Workflow.Api.Models.Entities;

namespace WWorkflow.Api.Infrastructure.Workflow.Handlers
{
    public class LiquidConfigurationHandler : INotificationHandler<EvaluatingLiquidExpression>
    {
        public Task Handle(EvaluatingLiquidExpression notification, CancellationToken cancellationToken)
        {
            var memberAccessStrategy = notification.TemplateContext.Options.MemberAccessStrategy; 
            memberAccessStrategy.Register<CaseDto>();
            memberAccessStrategy.Register<FileEntity>();
            
            return Task.CompletedTask;
        }
    }
}