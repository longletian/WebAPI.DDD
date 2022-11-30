using System.Threading;
using System.Threading.Tasks;
using Elsa.Activities.Http.Models;
using Elsa.Models;
using Elsa.Providers.Activities;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.AspNetCore.Http;
using Workflow.Api.Infrastructure;
using Workflow.Api.Models;
using Workflow.Api.Models.Dtos;
using Workflow.Api.Models.Entities;
using Case = DotLiquid.Tags.Case;

namespace Workflow.Api.Workflow.Providers
{
    public class CreateCaseProvider : WorkflowContextRefresher<CaseDto>
    {
        private readonly ICaseRepository caseRepository;

        public CreateCaseProvider(ICaseRepository _caseRepository)
        {
            caseRepository = _caseRepository;
        }

        public override async ValueTask<CaseDto> LoadAsync(LoadWorkflowContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await caseRepository.GetCaseEntityById(context.ContextId);
        }

        public override async ValueTask<string> SaveAsync(SaveWorkflowContext<CaseDto> context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            WorkflowInstance workflowInstance = context.WorkflowExecutionContext.WorkflowInstance;

            WorkflowCaseInstance instance = new WorkflowCaseInstance()
            {
                WorkflowInstanceId = workflowInstance.Id
            };

            CaseDto caseDto = ((HttpRequestModel)context.WorkflowExecutionContext.Input!).GetBody<CaseDto>();
            await caseRepository.ReportCase(caseDto, instance, cancellationToken);
            return "";
        }
    }
}
