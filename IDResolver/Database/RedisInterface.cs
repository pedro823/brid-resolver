using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace IDResolver.Database
{
    public static class RedisDatabase
    {
        public class CachedValue<T>
        {
            public T Data { get; set; }
        }

        public static ConnectionMultiplexer Multiplexer { get; private set; }

        public static void Initialize(string redisHost)
        {
            while (true)
            {
                try
                {
                    Multiplexer = ConnectionMultiplexer.Connect(redisHost);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not connect to redis service at {redisHost}. Retrying in 3s...");
                    System.Threading.Thread.Sleep(3000);
                }
            }
        }

        public static bool HasKey(string key)
        {
            var redis = Multiplexer.GetDatabase();
            return redis.KeyExists(key);
        }

        public static Task Set<T>(string key, T value, TimeSpan? ttl = null)
        {
            var serialized = DataPacker.Serialize(new CachedValue<T> {Data = value});
            var redis = Multiplexer.GetDatabase();
            return redis.StringSetAsync(key, serialized, ttl);
        }

        public static async Task<CachedValue<T>> Get<T>(string key)
        {
            var redis = Multiplexer.GetDatabase();
            var data = await redis.StringGetAsync(key);
            return Deserialize<T>(data);
        }

        private static CachedValue<T> Deserialize<T>(string data)
        {
            return string.IsNullOrEmpty(data) ? null : DataPacker.Deserialize<CachedValue<T>>(data);
        }
    }

    public static class DataPacker
    {
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, _serializerSettings);
        }

        public static T Deserialize<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized, _serializerSettings);
        }
    }
}