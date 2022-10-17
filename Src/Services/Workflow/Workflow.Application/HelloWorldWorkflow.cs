using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Workflow.Application
{
    public class HelloWorldWorkflow : IWorkflow
    {

        //public string Id => "HelloWorld";
        //public int Version => 1;

        //public void Build(IWorkflowBuilder<object> builder)
        //{
        //    builder
        //        .StartWith<HelloWorld>();
        //}


        public string Id => throw new NotImplementedException();

        public int Version => throw new NotImplementedException();

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<HelloWorld>();
        }
    }
}
