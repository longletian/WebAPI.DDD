using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfrastructureBase.Data
{
    public interface IDaprRepository<T> where T : class
    {
        Task<T> GetStateAsync(string Key);

        Task<(T,string)> GetStateAndETagAsync(string Key);

        Task SetStateAsync(string Key, T t);

        Task SetStateAsync(string Key, List<T> t);

        Task<bool> TrySaveStateAsync(string Key, T t, string ETag);

        Task<bool> TrySaveStateAsync(string Key, List<T> t, string ETag);
    }
}
