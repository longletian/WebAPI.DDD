using Microsoft.EntityFrameworkCore;
using Workflow.Api.Models.Entities;

namespace Workflow.Api.Infrastructure.Data
{
    public class WorkContext : DbContext
    {
        public WorkContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WorkflowCaseInstance> WorkflowCaseInstances { get; set; } = default!;
    }
}