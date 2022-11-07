using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Design;
using Elsa.Expressions;
using Elsa.Server.Api.Endpoints.WorkflowProviders;
using Elsa.Services;
using Elsa.Services.Models;
using InfrastructureBase;
using InfrastructureBase.Data.Utils;
using Microsoft.AspNetCore.Http;
using Workflow.Api.Models;

namespace Workflow.Api.Workflow.Activities
{
    public class FileUploadActivtity: Activity
    {
        private readonly IHttpService httpService;
        
        // [ActivityProperty(Hint = "Enter an expression that evaluates to the name of the user to create.")]
        // public WorkflowExpression<string> UserName
        // {
        //     get => GetState<WorkflowExpression<string>>();
        //     set => SetState(value);
        // }
        [ActivityInput(
                Hint = "附件上传",
                UIHint = ActivityInputUIHints.MultiText,
                DefaultSyntax = SyntaxNames.JavaScript,
                
                SupportedSyntaxes = new[] { SyntaxNames.Json,SyntaxNames.JavaScript, SyntaxNames.Liquid  },
                ConsiderValuesAsOutcomes = true
            )
        ]
        public ICollection<IFormFile> attachmentFiles { get; set; }
        
        [ActivityOutput] public ResponseData responseData { get; set; }

        public FileUploadActivtity(IHttpService _httpService)
        {
            httpService = _httpService;
        }
        
        /// <summary>
        /// 有返回值的执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Content-Type","multipart/form-data"}
            };

            Dictionary<string, string> dicts = new Dictionary<string, string>()
            {
                { "relativePath","/attenchments"}
            };

            responseData = await httpService.HttpRequestAsync(
                $"{AppSettingConfig.GetSection("FileConfig:BaseUrl")}/api/fileupload",HttpMethod.Post,dicts,headers);
            if (responseData != null)
            {
                
            }
            return Done();
        }

        /// <summary>
        /// 操作回退
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override async ValueTask<IActivityExecutionResult> OnResumeAsync(ActivityExecutionContext context)
        {
            // return base.OnResumeAsync(context);
            return Done();
        }
        
    }
}