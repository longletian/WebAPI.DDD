using Dapr.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfrastructureBase.Data
{
    public class DaprRepository<T> : IDaprRepository<T>  where T : class
    {
        private const string PubSubName = "";
        private const string StateName = "";
        private readonly DaprClient daprClient;
        private readonly ILogger<DaprRepository<T>> logger;

        public DaprRepository(DaprClient _daprClient, ILogger<DaprRepository<T>> logger)
        {
            daprClient = _daprClient ?? throw new ArgumentNullException(nameof(DaprClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<(T, string)> GetStateAndETagAsync(string Key)
        {
            return await daprClient.GetStateAndETagAsync<T>(StateName, Key);
        }

        public async Task<T> GetStateAsync(string Key)
        {
            return await daprClient.GetStateAsync<T>(StateName, Key);
        }

        public async Task SetStateAsync(string Key, T t)
        {
            await daprClient.SaveStateAsync(StateName, Key, t);
        }

        public async Task SetStateAsync(string Key, List<T> t)
        {
            await daprClient.SaveStateAsync(StateName, Key, t);
        }

        public async Task<bool> TrySaveStateAsync(string Key, T t, string ETag)
        {
            return await daprClient.TrySaveStateAsync(StateName, Key,t, ETag);
        }

        public async Task<bool> TrySaveStateAsync(string Key, List<T> t, string ETag)
        {
            return await daprClient.TrySaveStateAsync(StateName, Key, t, ETag);
        }


    }
}
