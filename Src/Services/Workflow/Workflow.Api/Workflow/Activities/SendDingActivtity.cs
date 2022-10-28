using System;
using System.Threading.Tasks;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Design;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;
using InfrastructureBase;
using InfrastructureBase.Data.Utils;
using RestSharp;
using Workflow.Api.Models;

namespace Workflow.Api.Workflow.Activities
{
    /// <summary>
    /// 发送浙政钉消息
    /// </summary>
    [Activity(Category = "Ding",Description = "发送浙政钉消息",DisplayName = "发送浙政钉消息")]
    public class SendDingActivtity : Activity
    {
        private readonly IHttpService httpService;

        public SendDingActivtity(IHttpService _httpService)
        {
            httpService = _httpService;
        }

        // [ActivityProperty(Hint = "Enter an expression that evaluates to the name of the user to create.")]
        // public WorkflowExpression<string> UserName
        // {
        //     get => GetState<WorkflowExpression<string>>();
        //     set => SetState(value);
        // }
        [ActivityInput(
            Label = "钉钉请求实体",
            Hint = "发送钉钉",
            UIHint = ActivityInputUIHints.MultiText,
            DefaultSyntax = SyntaxNames.Json,
            SupportedSyntaxes = new[] { SyntaxNames.Json })]
        public SendDingDto? sendDingDto { get; set; }
        
        [ActivityOutput(Name = "响应返回")] public ResponseData responseData { get; set; }

        /// <summary>
        /// 有返回值的执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            sendDingDto.Body = new BodyContent { text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") };
            responseData = await httpService.HttpRequestAsync<SendDingDto>(
                AppSettingConfig.GetSection("DingTalkConfig:BaseUrl").ToString(),
                sendDingDto);
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