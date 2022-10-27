using Microsoft.EntityFrameworkCore;
using Workflow.Api.Models.Entities;

namespace Workflow.Api.Infrastructure.Data
{
    public class WorkContext : DbContext
    {
        /// <summary>
        /// 迁移 需要引入efcore tools
        /// Add-Migration init-migration -c WorkContext -o .\Src\Services\Workflow\Workflow.Api\Infrastructure\Data
        /// </summary>
        /// <param name="options"></param>
        public WorkContext(DbContextOptions<WorkContext> options) : base(options)
        {
        }

        public DbSet<WorkflowCaseInstance> WorkflowCaseInstances { get; set; } = default!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}