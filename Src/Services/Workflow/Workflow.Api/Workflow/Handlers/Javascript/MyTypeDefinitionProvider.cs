using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Scripting.JavaScript.Services;
using Workflow.Api.Models.Entities;

namespace WWorkflow.Api.Infrastructure.Workflow
{
    public class MyTypeDefinitionProvider:TypeDefinitionProvider 
    {
        public override ValueTask<IEnumerable<Type>> CollectTypesAsync(TypeDefinitionContext context, CancellationToken cancellationToken = default)
        {
            var types = new[] { typeof(FileEntity) };
            return new ValueTask<IEnumerable<Type>>(types);
        }
    }
}