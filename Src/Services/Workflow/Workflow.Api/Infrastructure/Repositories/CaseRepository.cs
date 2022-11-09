using System;
using System.Threading;
using System.Threading.Tasks;
using InfrastructureBase;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.Infrastructure.Data;
using Workflow.Api.Models;
using Workflow.Api.Models.Dtos;
using Workflow.Api.Models.Entities;

namespace Workflow.Api.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CaseRepository : ICaseRepository
    {
        private readonly WorkContext workContext;

        public CaseRepository(IDbContextFactory<WorkContext> _workFactory)
        {
            workContext = _workFactory.CreateDbContext();
        }

        /// <summary>
        /// 事件上报
        /// </summary>
        /// <param name="caseDto"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public async Task ReportCase(CaseDto caseDto, WorkflowCaseInstance instance,
            CancellationToken cancellationToken = default)
        {
            // WorkContext workContext = workFactory.CreateDbContext();
            
            Case caseEntity = caseDto.MapTo<Case>();
            try
            {
                await this.workContext.Database.BeginTransactionAsync();
                this.workContext.Cases.AddAsync(caseEntity,cancellationToken);

                instance.CaseId = caseEntity.Id;
                await this.workContext.WorkflowCaseInstances.AddAsync(instance, cancellationToken);
                await this.workContext.SaveChangesAsync(cancellationToken);
                await this.workContext.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                await this.workContext.Database.RollbackTransactionAsync();
                throw;
            }
        }
    }
}