using System;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Workflow.Api.Infrastructure.Data;

namespace Workflow.Api.Infrastructure.Data.StartupTasks
{
    /// <summary>
    /// 自动化迁移
    /// </summary>
    public class RunWorkMigrations : IStartupTask
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


    /// <summary>
    /// 创建后台异步的方式
    /// </summary>

    public class RunWorkMigrationsV1 : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        public RunWorkMigrationsV1(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope =serviceProvider.CreateScope())
            {
                var dbContext= scope.ServiceProvider.GetService<WorkContext>();
                await dbContext.Database.MigrateAsync();
                await dbContext.DisposeAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}