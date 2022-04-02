using Newtonsoft.Json;
using TG.Helpers.ServiceExtensions;

namespace TG.Common.Services.CacheService.Distributed
{
    public class RedisCacheService : IDistributedCacheService
    {
        private readonly RedisServer redisServer;

        public RedisCacheService(RedisServer redisServer)
        {
            this.redisServer = redisServer;
        }

        public void Add(string key, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            redisServer.Database.StringSet(key, json);
        }

        public bool Any(string key)
        {
            return redisServer.Database.KeyExists(key);
        }

        public void Clear()
        {
            redisServer.FlushDatabase();
        }

        public T Get<T>(string key)
        {
            if (Any(key))
            {
                string json = redisServer.Database.StringGet(key);
                return JsonConvert.DeserializeObject<T>(key);
            }

            return default;
        }

        public void Remove(string key)
        {
            redisServer.Database.KeyDelete(key);
        }
    }
}
