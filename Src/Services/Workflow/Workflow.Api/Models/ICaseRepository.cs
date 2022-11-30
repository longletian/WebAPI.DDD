using System.Threading;
using System.Threading.Tasks;
using Workflow.Api.Models.Dtos;
using Workflow.Api.Models.Entities;
using Case = Workflow.Api.Models.Entities.Case;

namespace Workflow.Api.Models
{
    public interface ICaseRepository
    {

        /// <summary>
        /// 事件上报
        /// </summary>
        /// <param name="caseDto"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task ReportCase(CaseDto caseDto, WorkflowCaseInstance instance,CancellationToken cancellationToken = default);

        Task<CaseDto> GetCaseEntityById(string caseId);
    }
}