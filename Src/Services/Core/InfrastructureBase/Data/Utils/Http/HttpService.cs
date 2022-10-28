using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace InfrastructureBase.Data.Utils
{
    public class HttpService : IHttpService
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
        public async Task<ResponseData> HttpRequestAsync(string baseUrl, Method method,
            Dictionary<string, string> dictParams = null, Dictionary<string, string> headers = null,
            bool isEnableHttps = true)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest();
            if (headers != null && headers.Count > 0)
                request.AddHeaders(headers);
            if (!isEnableHttps)
            {
                //关闭https验证
                // client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            }
            if (dictParams != null && dictParams.Count > 0)
            {
                if (method == Method.Get)
                {
                    foreach (var keyValuePair in dictParams)
                        request.AddQueryParameter(keyValuePair.Key,keyValuePair.Value);
                }
                else if (method==Method.Post)
                {
                    foreach (var keyValuePair in dictParams)
                        request.AddParameter(keyValuePair.Key,keyValuePair.Value);
                }
            }
            request.Method = method;
            var restResponse= await client.ExecuteAsync(request);
            string content = restResponse.Content;
            if (!string.IsNullOrEmpty(content))
                return new ResponseData { MsgCode = 0,Message = "请求成功",Data = content};
            else
            {
                Log.Error($"接口地址{baseUrl}\n接口参数，接口返回数据{JsonConvert.SerializeObject(restResponse)}");
                return new ResponseData { MsgCode = 1,Message = "请求失败",};
            }
        }
        
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="baseUrl">基础地址</param>
        /// <param name="formFile">请求文件</param>
        /// <param name="dictParams">请求文件</param>
        /// <param name="headers">请求头</param>
        /// <param name="isEnableHttps">是否验证https</param>
        /// <returns></returns>
        public async Task<ResponseData> HttpRequestAsync(string baseUrl,IFormFile formFile, Dictionary<string, string> dictParams = null,
            Dictionary<string, string> headers = null, bool isEnableHttps = true)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest();
            if (headers != null && headers.Count > 0)
                request.AddHeaders(headers);
            if (!isEnableHttps)
            {
                //关闭https验证
                // client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            }
            if (dictParams != null && dictParams.Count > 0)
            {
                foreach (var keyValuePair in dictParams)
                    request.AddQueryParameter(keyValuePair.Key,keyValuePair.Value);
            }

            if (formFile != null)
                request.AddFile("file",()=> formFile.OpenReadStream(), formFile.Name, formFile.ContentType);
            
            request.Method = Method.Post;
            var restResponse= await client.ExecuteAsync(request);
            string content = restResponse.Content;
            if (!string.IsNullOrEmpty(content))
                return new ResponseData { MsgCode = 0,Message = "请求成功",Data = content};
            else
            {
                Log.Error($"接口地址{baseUrl}\n接口参数，接口返回数据{JsonConvert.SerializeObject(restResponse)}");
                return new ResponseData { MsgCode = 1,Message = "请求失败",};
            }
        }
        
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="baseUrl">基础地址</param>
        /// <param name="jsonBody">请求对象</param>
        /// <param name="dictParams">请求对象</param>
        /// <param name="headers">请求头</param>
        /// <param name="isEnableHttps">是否验证https</param>
        /// <returns></returns>
        public async Task<ResponseData> HttpRequestAsync<T>(string baseUrl, T jsonBody,Dictionary<string, string> dictParams = null,
            Dictionary<string, string> headers = null, bool isEnableHttps = true) where T : class
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest();
            if (headers != null && headers.Count > 0)
                request.AddHeaders(headers);
            if (!isEnableHttps)
            {
                //关闭https验证
                // client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            }
            if (dictParams != null && dictParams.Count > 0)
            {
                foreach (var keyValuePair in dictParams)
                    request.AddQueryParameter(keyValuePair.Key,keyValuePair.Value);
            }

            if (jsonBody != null)
                request.AddJsonBody(jsonBody);
            
            request.Method = Method.Post;
            var restResponse= await client.ExecuteAsync(request);
            string content = restResponse.Content;
            if (!string.IsNullOrEmpty(content))
                return new ResponseData { MsgCode = 0,Message = "请求成功",Data = content};
            else
            {
                Log.Error($"接口地址{baseUrl}\n接口参数，接口返回数据{JsonConvert.SerializeObject(restResponse)}");
                return new ResponseData { MsgCode = 1,Message = "请求失败",};
            }
        }

    }
}