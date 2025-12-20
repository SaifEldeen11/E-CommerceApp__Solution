using Domain.RepoInterfaces;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string cacheKey)
        {
            return await _cacheRepository.GetAsync(cacheKey);
        }

        public async Task SetAsync(string cacheKey, object CacheValue, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(CacheValue);
            await _cacheRepository.SetAsync(cacheKey, value, timeToLive);
        }
    }
}
