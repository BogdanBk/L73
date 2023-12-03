using System;
using System.Collections.Generic;

class FunctionCache<TKey, TResult>
{
    private Dictionary<TKey, CacheItem> cache = new Dictionary<TKey, CacheItem>();

    public class CacheItem
    {
        public TResult Result { get; set; }
        public DateTime ExpirationTime { get; set; }
    }

    public delegate TResult ExpensiveFunction(TKey key);

    public TResult GetOrCompute(TKey key, ExpensiveFunction function, TimeSpan cacheDuration)
    {
        if (cache.TryGetValue(key, out var cachedResult) && DateTime.Now < cachedResult.ExpirationTime)
        {
            Console.WriteLine($"Result for key '{key}' found in cache.");
            return cachedResult.Result;
        }

        Console.WriteLine($"Result for key '{key}' not found in cache. Caching result.");
        TResult result = function(key);

        cache[key] = new CacheItem
        {
            Result = result,
            ExpirationTime = DateTime.Now.Add(cacheDuration)
        };

        return result;
    }
}
