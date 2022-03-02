using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InfrastructureBase.Data
{
    public interface IDaprRepository
    {
        Task<T> GetStateAsync<T>(string Key);

        Task<(T,string)> GetStateAndETagAsync<T>(string Key);

        Task SetStateAsync<T>(string Key, T t);

        Task SetStateAsync<T>(string Key, List<T> t);

        Task DelStateAsync(string Key);

        Task<bool> TryDeleteStateAsync(string Key, string ETag);

        Task<bool> TrySaveStateAsync<T>(string Key, T t, string ETag);

        Task<bool> TrySaveStateAsync<T>(string Key, List<T> t, string ETag);

        Task<T2> InvokeMethodAsync<T2,T>(HttpMethod method, string serviceName, string routeName, T t = default(T), string token = "");

        Task<T2> InvokeMethodAsync<T2, T>(HttpMethod method, string serviceName, string routeName, List<T> t = null, string token = "");

        Task<IEnumerable<T2>> InvokeMethodListAsync<T2, T>(HttpMethod method, string serviceName, string routeName, T t =default(T), string token = "");

        Task<T2> InvokeMethodListAsync<T2, T>(HttpMethod method, string serviceName, string routeName, List<T> t = null, string token = "");

        Task<IEnumerable<T2>> InvokeMethodByParamsAsync<T2, T>(HttpMethod method, string serviceName, string routeName, string token = "", params object[] parms);

        Task<T2> InvokeMethodAsync<T2, T>(HttpMethod method, string serviceName, string routeName, string token = "", params object[] parms);
    }
}
