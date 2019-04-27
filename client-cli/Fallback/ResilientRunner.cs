using Polly;
using System;
using System.Net.Http;
using System.Threading;

namespace PollyDemo.Client.Fallback
{
    static class ResilientRunner
    {
        private const int DEFAULT_NOTHING = -1;

        internal static void Run()
        {
            var apiClient = new ApiClient(Settings.Instance.BaseUrl);
            
            var fallbackPolicy = Policy
                .HandleResult(0)
                .Or<HttpRequestException>()
                .Fallback(()  => throw new ApplicationException("Got nothing!"));
                // .Fallback(()  => DEFAULT_NOTHING);

            var i = 0;

            while (true)
            {
                i++;

                try
                {
                    Console.Write($"[{i:00}] ");
                    var doubleOrNothing = fallbackPolicy.Execute(() => apiClient.DoubleOrNothing(i));
                    Console.WriteLine($"Double or nothing of {i} is {doubleOrNothing}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" ** {ex.Message} **");
                }

                Thread.Sleep(1000);
            }
        }
    }
}
