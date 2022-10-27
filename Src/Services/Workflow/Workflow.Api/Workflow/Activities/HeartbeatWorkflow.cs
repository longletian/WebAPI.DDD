using Elsa.Activities.Console;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using NodaTime;

namespace Workflow.Api.Infrastructure.Workflow.Activities
{
    public class HeartbeatWorkflow : IWorkflow
    {
        private readonly IClock _clock;
        public HeartbeatWorkflow(IClock clock) => _clock = clock;

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="builder"></param>
        public void Build(IWorkflowBuilder builder) =>
            builder
                .Timer(Duration.FromSeconds(10))
                .WriteLine(context => $"Heartbeat at {_clock.GetCurrentInstant()}");
    }
    
}