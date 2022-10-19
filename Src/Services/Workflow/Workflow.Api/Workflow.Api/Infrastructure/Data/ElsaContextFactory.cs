using System;
using System.IO;
using Elsa.Persistence.EntityFramework.Core;
using InfrastructureBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Workflow.Api.Infrastructure
{
    /// <summary>
    /// efcore自定义迁移
    /// </summary>
    public class ElsaContextFactory : IDesignTimeDbContextFactory<ElsaContext>
    {
        public ElsaContext CreateDbContext(string[] args)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            var connectionString = AppSettingConfig.GetConnStrings("MysqlWorkCon");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

            dbContextBuilder.UseMySql(connectionString, serverVersion, config => config.MigrationsAssembly(typeof(Elsa.Persistence.EntityFramework.MySql.MySqlElsaContextFactory).Assembly.FullName));

            return new ElsaContext(dbContextBuilder.Options);
        }
    }
}