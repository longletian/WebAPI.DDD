using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace InfrastructureBase.Data.Utils
{
    public interface IHttpService
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="baseUrl">基础地址</param>
        /// <param name="method">方法 Post Get</param>
        /// <param name="dictParams">参数</param>
        /// <param name="headers">请求头</param>
        /// <param name="isEnableHttps">是否验证https</param>
        /// <returns></returns>
        Task<ResponseData> HttpRequestAsync(string baseUrl,Method method,Dictionary<string,string> dictParams=null,Dictionary<string,string>headers=null, bool isEnableHttps=true);
        
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="baseUrl">基础地址</param>
        /// <param name="formFile">请求文件</param>
        /// <param name="dictParams">请求文件</param>
        /// <param name="headers">请求头</param>
        /// <param name="isEnableHttps">是否验证https</param>
        /// <returns></returns>
        Task<ResponseData> HttpRequestAsync(string baseUrl,IFormFile formFile,Dictionary<string, string> dictParams = null,Dictionary<string,string>headers=null, bool isEnableHttps=true);
        
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="baseUrl">基础地址</param>
        /// <param name="jsonBody">请求对象</param>
        /// <param name="headers">请求头</param>
        /// <param name="isEnableHttps">是否验证https</param>
        /// <returns></returns>
        Task<ResponseData> HttpRequestAsync<T>(string baseUrl, T jsonBody,Dictionary<string, string> dictParams = null,
            Dictionary<string, string> headers = null, bool isEnableHttps = true) where T : class;
    }
}