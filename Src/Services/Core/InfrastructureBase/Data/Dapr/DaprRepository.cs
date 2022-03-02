using Dapr.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InfrastructureBase.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DaprRepository : IDaprRepository, ISingletonDependency
    {
        private const string StateName = "statestore";
        private const string PubSubName = "pubsub";
        private readonly DaprClient daprClient;
        private readonly ILogger<DaprRepository> logger;

        public DaprRepository(DaprClient _daprClient, ILogger<DaprRepository> logger)
        {
            daprClient = _daprClient ?? throw new ArgumentNullException(nameof(DaprClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<(T, string)> GetStateAndETagAsync<T>(string Key)
        {
            return await daprClient.GetStateAndETagAsync<T>(StateName, Key);
        }

        public async Task<T> GetStateAsync<T>(string Key)
        {
            return await daprClient.GetStateAsync<T>(StateName, Key);
        }

        public async Task SetStateAsync<T>(string Key, T t)
        {
            await daprClient.SaveStateAsync(StateName, Key, t);
        }

        public async Task DelStateAsync(string Key)
        {
            await daprClient.DeleteStateAsync(StateName, Key);
        }

        public async Task<bool> TryDeleteStateAsync(string Key,string ETag)
        {
            return await daprClient.TryDeleteStateAsync(StateName, Key, ETag);
        }

        public async Task SetStateAsync<T>(string Key, List<T> t)
        {
            await daprClient.SaveStateAsync(StateName, Key, t);
        }

        public async Task<bool> TrySaveStateAsync<T>(string Key, T t, string ETag)
        {
            return await daprClient.TrySaveStateAsync(StateName, Key, t, ETag);
        }

        public async Task<bool> TrySaveStateAsync<T>(string Key, List<T> t, string ETag)
        {
            return await daprClient.TrySaveStateAsync(StateName, Key, t, ETag);
        }

        public async Task<T2> InvokeMethodAsync<T2, T>(HttpMethod method, string serviceName, string routeName, T t = default(T), string token = "")
        {
            var request = daprClient.CreateInvokeMethodRequest(serviceName, routeName, t);
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            return await daprClient.InvokeMethodAsync<T2>(method, serviceName, routeName);
        }

        public async Task<T2> InvokeMethodAsync<T2, T>(HttpMethod method, string serviceName, string routeName, List<T> t = null, string token = "")
        {
            var request = daprClient.CreateInvokeMethodRequest(serviceName, routeName, t);
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            return await daprClient.InvokeMethodAsync<T2>(method, serviceName, routeName);
        }

        public async Task<IEnumerable<T2>> InvokeMethodListAsync<T2, T>(HttpMethod method, string serviceName, string routeName, T t = default(T), string token = "")
        {
            var request = daprClient.CreateInvokeMethodRequest(serviceName, routeName, t);
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            return await daprClient.InvokeMethodAsync<IEnumerable<T2>>(method, serviceName, routeName);
        }

        public async Task<T2> InvokeMethodListAsync<T2, T>(HttpMethod method, string serviceName, string routeName, List<T> t = null, string token = "")
        {
            var request = daprClient.CreateInvokeMethodRequest(serviceName, routeName, t);
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            return await daprClient.InvokeMethodAsync<T2>(method, serviceName, routeName);
        }

        public async Task<IEnumerable<T2>> InvokeMethodByParamsAsync<T2, T>(HttpMethod method, string serviceName, string routeName, string token = "", params object[] parms)
        {
            var request = daprClient.CreateInvokeMethodRequest(serviceName, routeName, parms);
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            return await daprClient.InvokeMethodAsync<IEnumerable<T2>>(method, serviceName, routeName);
        }

        public async Task<T2> InvokeMethodAsync<T2, T>(HttpMethod method, string serviceName, string routeName, string token = "", params object[] parms)
        {
            var request = daprClient.CreateInvokeMethodRequest(serviceName, routeName, parms);
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            return await daprClient.InvokeMethodAsync<T2>(method, serviceName, routeName);
        }
    }
}
