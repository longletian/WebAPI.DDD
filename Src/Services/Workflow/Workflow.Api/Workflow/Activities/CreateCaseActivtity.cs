using Elsa.Services;
using InfrastructureBase;

namespace Workflow.Api.Workflow.Activities
{
    public class CreateCaseActivtity:Activity
    {
        private readonly IEventBus eventBus;
        public CreateCaseActivtity(IEventBus _eventBus)
        {
            eventBus = _eventBus;
        }

    }
}