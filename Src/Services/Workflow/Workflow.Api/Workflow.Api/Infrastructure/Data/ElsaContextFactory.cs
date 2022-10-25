using System;
using System.IO;
using Elsa.Persistence.EntityFramework.Core;
using Elsa.Persistence.EntityFramework.MySql;
using InfrastructureBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Workflow.Api.Infrastructure
{
    /// <summary>
    /// efcore自定义迁移
    /// </summary>
    public class ElsaContextFactory : IDesignTimeDbContextFactory<ElsaContext>
    {
    
        // 迁移命令，移动到context指令文件夹
        // dotnet ef database update -c ElsaContext -p ../../../Workflow.Api
        public ElsaContext CreateDbContext(string[] args)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            var connectionString = AppSettingConfig.GetConnStrings("MysqlWorkCon");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

            dbContextBuilder.UseMySql(connectionString, serverVersion, config => config
                .MigrationsAssembly(typeof(MySqlElsaContextFactory).Assembly.GetName().Name)
                .MigrationsHistoryTable(ElsaContext.MigrationsHistoryTable, ElsaContext.ElsaSchema)
                .SchemaBehavior(MySqlSchemaBehavior.Ignore));

            return new ElsaContext(dbContextBuilder.Options);
        }
    }
}