using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Common.Services.CacheService.Memory
{
    public interface IMemoryCacheService
    {
        T Get<T>(string key);
        void Add<T>(string key, T value, DateTime expires) where T : class;
        bool IsSet(string key);
        void Remove(string key);
    }
}
