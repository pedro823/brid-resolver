using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace IDResolver.Database
{
    public static class RedisDatabase
    {
        public class CacheValue<T>
        {
            public T Data;
        }

        public static ConnectionMultiplexer Multiplexer { get; private set; }

        public static void Initialize(string redisHost)
        {
            Multiplexer = ConnectionMultiplexer.Connect(redisHost);
        }

        public static bool HasKey(string key)
        {
            var redis = Multiplexer.GetDatabase();
            return redis.KeyExists(key);
        }

        public static Task Set(string key, object value, TimeSpan? ttl = null)
        {
            var serialized = DataPacker.Serialize(value);
            var redis = Multiplexer.GetDatabase();
            return redis.StringSetAsync(key, serialized, ttl);
        }

        public static async Task<T> Get<T>(string key)
        {
            var redis = Multiplexer.GetDatabase();
            var data = await redis.StringGetAsync(key);
            return DataPacker.Deserialize<T>(data);
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