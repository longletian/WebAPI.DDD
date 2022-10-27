using Microsoft.EntityFrameworkCore;
using Workflow.Api.Infrastructure.Data;
using Workflow.Api.Models;

namespace Workflow.Api.Infrastructure
{
    public class WorkflowRepository: IWorkflowRepository
    {
        private readonly IDbContextFactory<WorkContext> workContextFactroy;
        private readonly WorkContext workContext;
        private bool _disposed;

        public WorkflowRepository(IDbContextFactory<WorkContext> _workContextFactroy)
        {
            workContextFactroy = _workContextFactroy;
        }

        /// <summary>
        /// 需要释放
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;
            this.workContext.Dispose();
            _disposed = true;
        }
    }
}
