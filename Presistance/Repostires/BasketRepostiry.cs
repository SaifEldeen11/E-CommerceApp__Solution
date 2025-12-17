using Domain.Models.BasketModule;
using Domain.RepoInterfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance.Repostires
{
    public class BasketRepostiry(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id,JsonBasket,TimeToLive ?? TimeSpan.FromDays(2));

            if(IsCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            return null;
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<Basket?> GetBasketAsync(string key)
        {
            var basket = await _database.StringGetAsync(key);

            if (basket.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<Basket>(basket!);
        }
    }
}
