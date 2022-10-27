using System.Threading;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.EntityFrameworkCore;
using Workflow.Api.Infrastructure.Data;

namespace Workflow.Api.Infrastructure.Data.StartupTasks
{
    /// <summary>
    /// 自动化迁移
    /// </summary>
    public class RunWorkMigrations: IStartupTask
    {
        private readonly IDbContextFactory<WorkContext> _dbContextFactory;

        public RunWorkMigrations(IDbContextFactory<WorkContext> dbContextFactoryFactory)
        {
            _dbContextFactory = dbContextFactoryFactory;
        }

        public int Order => 0;
        
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            await dbContext.Database.MigrateAsync(cancellationToken);
            await dbContext.DisposeAsync();
        } 
    }
}