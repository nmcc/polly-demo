using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching.Memory;
using System;
using System.Threading;

namespace PollyDemo.Client.Cache
{
    class ResilientRunner
    {
        internal static void Run()
        {
            var apiClient = new ApiClient(Settings.Instance.BaseUrl);

            var memoryCacheProvider = new MemoryCacheProvider(new MemoryCache(new MemoryCacheOptions()));
            var cachePolicy = Policy.Cache(memoryCacheProvider, TimeSpan.FromSeconds(5));

            var i = 0;

            const string name = "NetPonto";

            while (true)
            {
                i++;

                Console.Write($"[{i:00}] ");
                string message = cachePolicy.Execute(
                    (ctx) => apiClient.SayHello(name),
                    new Context(operationKey: name));

                Console.WriteLine($"server said \"{message}\"");

                Thread.Sleep(1000);
            }
        }
    }
}
