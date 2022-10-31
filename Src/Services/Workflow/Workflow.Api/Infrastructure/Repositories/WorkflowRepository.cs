using Elsa.Persistence;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.Infrastructure.Data;
using Workflow.Api.Models;

namespace Workflow.Api.Infrastructure
{
    public class WorkflowRepository: IWorkflowRepository
    {
        private readonly IDbContextFactory<WorkContext> workContextFactroy;
        private readonly IWorkflowDefinitionStore workflowDefinitionStore;
        private readonly IWorkflowInstanceStore  workflowInstanceStore;
        private readonly IWorkflowExecutionLogStore workflowExecutionLogStore;
        private readonly IBookmarkStore bookmarkStore;
        private bool _disposed;
        private WorkContext workContext;

        public WorkflowRepository(
            IDbContextFactory<WorkContext> _workContextFactroy,
            IWorkflowDefinitionStore _workflowDefinitionStore,
            IWorkflowInstanceStore  _workflowInstanceStore,
            IWorkflowExecutionLogStore _workflowExecutionLogStore,
            IBookmarkStore _bookmarkStore
            )
        {
            workContextFactroy = _workContextFactroy;
            workflowDefinitionStore = _workflowDefinitionStore;
            workflowInstanceStore = _workflowInstanceStore;
            workflowExecutionLogStore = _workflowExecutionLogStore;
            bookmarkStore = _bookmarkStore;
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
