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
                
                // MigrateAsync 确保使用迁移创建数据库，并应用了所有迁移。
                // EnsureCreatedAsync 确保数据库在每次执行测试/原型之前处于干净状态，但数据库中的数据不会保留
                await dbContext.Database.MigrateAsync();
                // 初始化值
                // await dbContext.Database.EnsureCreatedAsync();
                // if (await dbContext.WorkflowActivityInstanceUsers.AnyAsync())
                //     return;
                // else
                // {
                // }
                await dbContext.DisposeAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}