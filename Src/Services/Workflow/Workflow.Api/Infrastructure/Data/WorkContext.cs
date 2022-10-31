using Microsoft.EntityFrameworkCore;
using Workflow.Api.Models.Entities;

namespace Workflow.Api.Infrastructure.Data
{
    public class WorkContext : DbContext
    {
        /// <summary>
        /// Ǩ�� ��Ҫ����efcore tools
        /// Add-Migration init-migration -c WorkContext -o .\Src\Services\Workflow\Workflow.Api\Infrastructure\Data
        /// </summary>
        /// <param name="options"></param>
        public WorkContext(DbContextOptions<WorkContext> options) : base(options)
        {
        }

        public DbSet<WorkflowCaseInstance> WorkflowCaseInstances { get; set; } = default!;

        public DbSet<WorkflowActivityInstanceUser> WorkflowActivityInstanceUsers { get; set; } = default!;

        /// <summary>
        /// 配置数据
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connectionString);
        }

        /// <summary>
        /// 实体配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkflowActivityInstanceUser>().HasKey(c =>
                new
                {
                    c.WorkflowActivityInstanceId, c.UserId
                });
        }
    }
}