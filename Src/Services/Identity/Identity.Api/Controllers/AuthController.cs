using Dapr;
using Dapr.Client;
using Identity.Application;
using Identity.Domain.IntegrationEvents;
using InfrastructureBase;
using InfrastructureBase.EventBus;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string PubSubName = "pubsub";
        private readonly IAuthService authService;

        #region 注入方式
        //private readonly IEventBus eventBus;
        //public AuthController(IEnumerable<IEventBus> eventBuses, IAuthService _authService)
        //{
        //    authService = _authService;
        //    eventBus = eventBuses.FirstOrDefault(h => h.GetType() == typeof(RabbitmqEventBus));
        //}
        #endregion

        private readonly IEventBus eventBus;
        public AuthController(Func<Type, IEventBus> _eventBus, IAuthService _authService)
        {
            authService = _authService;
            eventBus = _eventBus(typeof(RabbitmqEventBus));
        }


        [HttpGet, Route("token")]
        public async Task<ResponseData<string>> GetToken()
        {
            return await authService.GetToken();
        }

        [HttpGet, Route("test")]
        public ResponseData<string> TestData(string token)
        {
            Log.Information("进入方法");
            return authService.TestData(token);
        }

        [HttpGet, Route("pubsub")]
        public ResponseData<string> Test([FromServices] DaprClient daprClient)
        {
            try
            {
                daprClient.PublishEventAsync(PubSubName, "test_topic");
                return new ResponseData<string> { Message = "", MsgCode = 0 };
            }
            catch (System.Exception ex)
            {
                Log.Information(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 注意配置文件是否对应名称
        /// </summary>
        [Topic(PubSubName, "test_topic")]
        [HttpPost("sub")]
        public void TestPub()
        {
            Log.Information("进入pub方法");
        }


        [HttpGet,Route("")]
        public  void RabbitmqTest()
        {
            for (int i = 0; i < 10; i++)
            {
                UserEvent userEvent = new UserEvent($"username{i}", i);
                eventBus.PublishAsync(userEvent, nameof(UserEvent));
            }
        }

        [HttpGet,Route("test-sign")]
        public string TestSign(string str)
        {
            string strSign = $"appId=1Jkdialkngadiufjskay&nonce=ijwetojfndasosdgusafqdf&timestamp=1629620288&v=v1{str}";
            return SignStr("aaaaaaaaaaaaaaaaaa", strSign);
        }

        private string SignStr(string AUTH_TOKEN,string str)
        {
            byte[] key = Encoding.ASCII.GetBytes(AUTH_TOKEN);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(str);
            MemoryStream stream = new MemoryStream(byteArray);
            string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            return result;
        }
    }
}
