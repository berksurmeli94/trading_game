using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace TG.Common.Services.CacheService.Memory
{
    public class MemoryCacheService : IMemoryCacheService
    {

        private readonly IMemoryCache memcache;

        public MemoryCacheService(IMemoryCache memcache)
        {
            this.memcache = memcache;
        }

        public void Add<T>(string key, T value, DateTime expires) where T : class
        {
            memcache.Set(key, value, expires);
        }

        public T Get<T>(string key)
        {
            return memcache.Get<T>(key);
        }

        public bool IsSet(string key)
        {
            return memcache.Get(key) != null;
        }

        public void Remove(string key)
        {
            memcache.Remove(key);
        }
    }
}
