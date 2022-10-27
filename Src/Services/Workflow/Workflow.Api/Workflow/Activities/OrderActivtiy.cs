using System.Threading.Tasks;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Providers.WorkflowStorage;
using Elsa.Services;
using Elsa.Services.Models;
using Ordering.Domain;
using Oxygen.Client.ServerProxyFactory.Interface;

namespace Workflow.Api.Workflow.Activities
{
    [Activity(Category = "订单上报", Description = "订单上报")]
    public class OrderActivtiy : Activity
    {
        private readonly IEventBus eventBus;
        public OrderActivtiy(IEventBus _eventBus)
        {
            eventBus = _eventBus;
        }

        [ActivityInput(
            Label = "订单",
            Hint = "The document to archive",
            SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid },
            DefaultWorkflowStorageProvider = TransientWorkflowStorageProvider.ProviderName
        )]
        public Order Order { get; set; } = default!;
        
        
        protected override ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            return base.OnExecuteAsync(context);
        }

    }
}