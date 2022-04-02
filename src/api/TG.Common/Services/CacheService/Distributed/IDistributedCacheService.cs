namespace TG.Common.Services.CacheService.Distributed
{
    public interface IDistributedCacheService
    {
        T Get<T>(string key);
        void Add(string key, object data);
        void Remove(string key);
        void Clear();
        bool Any(string key);
    }
}
