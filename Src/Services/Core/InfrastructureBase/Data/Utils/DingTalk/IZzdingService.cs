using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace InfrastructureBase
{
    /// <summary>
    /// 浙政钉相关接口
    /// </summary>
    public interface IZzdingService
    {
        /// <summary>
        /// 基础封装浙政钉钉钉
        /// </summary>
        /// <param name="method"></param>
        /// <param name="canonicalURI"></param>
        /// <param name="requestParas"></param>
        /// <returns></returns>
        Task<string> DingGovRequestAsync(Method method, string canonicalURI,
            Dictionary<string, string> requestParas = null, string appkey = "", string appSecret = "");
    }
}