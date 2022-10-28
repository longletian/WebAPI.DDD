using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestSharp;

namespace InfrastructureBase.Data.Utils.DingTalk
{
    public class ZzdingService : IZzdingService
    {
        private readonly ZDingTalkOption zDingTalkOption;

        public ZzdingService(IOptionsMonitor<ZDingTalkOption> _zDingTalkOption)
        {
            zDingTalkOption = _zDingTalkOption.CurrentValue;
        }

        /// <summary>
        /// 政务钉请求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="canonicalURI"></param>
        /// <param name="requestParas"></param>
        /// <returns></returns>
        public async Task<string> DingGovRequestAsync(Method method, string canonicalURI,
            Dictionary<string, string> requestParas = null,string appkey="",string appSecret="")
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            var nonce = ConvertDateTimeToInt(DateTime.Now) + "1234";
            var requestUrl = zDingTalkOption.ZDingBaseUrl;
            var client = new RestClient(requestUrl);
            try
            {
                var request = new RestRequest(canonicalURI, method);
                var bytesToSign = $"{method}\n{timestamp}\n{nonce}\n{canonicalURI}";
                if (requestParas?.Count() > 0)
                {
                    requestParas = requestParas.OrderBy(m => m.Key).ToDictionary(m => m.Key, p => p.Value);
                    //参数参与签名
                    bytesToSign += '\n' + DicionartyToUrlParameters(requestParas);
                }

                #region 请求头

                Dictionary<string, string> dict = new Dictionary<string, string>()
                {
                    { "X-Hmac-Auth-Timestamp", timestamp },
                    { "X-Hmac-Auth-Version", "1.0" },
                    { "X-Hmac-Auth-Nonce", nonce },
                    { "apiKey", appkey??zDingTalkOption.AppKey },
                    { "Content-Type", "application/json" },
                    { "X-Hmac-Auth-Signature", GetSignature(bytesToSign, appSecret??zDingTalkOption.AppSecret) },
                };
                client.AddDefaultHeaders(dict);

                #endregion

                if (method == Method.Post)
                {
                    var paras = new Dictionary<string, string>();
                    if (requestParas?.Count() > 0)
                    {
                        foreach (var dic in requestParas)
                        {
                            request.AddParameter(dic.Key, Uri.UnescapeDataString(dic.Value));
                        }
                    }
                }
                else if (method == Method.Get)
                {
                    requestUrl += $"?{DicionartyToUrlParameters(requestParas)}";
                }

                var response = await client.ExecuteAsync(request);
                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                    return response.Content;
                return "";
            }
            catch (Exception ex)
            {
                //记录日志
                throw;
            }
        }

        /// <summary>
        /// 对参数进行url字符拼接
        /// </summary>
        /// <param name="dicParameter"></param>
        /// <returns></returns>
        private string DicionartyToUrlParameters(Dictionary<string, string> dicParameter)
        {
            var HttpRequestParams = string.Empty;
            if (dicParameter != null)
            {
                foreach (var item in dicParameter)
                {
                    if (!string.IsNullOrEmpty(HttpRequestParams))
                        HttpRequestParams += "&";
                    HttpRequestParams = string.Format("{0}{1}={2}", HttpRequestParams, item.Key, item.Value);
                }
            }
            return HttpRequestParams;
        }

        /// <summary>
        /// 转化为时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private long ConvertDateTimeToInt(DateTime time)
        {
            System.DateTime startTime =
                TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000; //除10000调整为13位      
            return t;
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="message">将bytesToSign作为消息</param>
        /// <param name="secret">将SecretKey作为秘钥</param>
        /// <returns></returns>
        private string GetSignature(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
    }
}