using Polly;
using System;
using System.Threading;

namespace PollyDemo.Client.Fallback
{
    class ResilientRunner
    {
        internal static void Run()
        {
            var apiClient = new ApiClient(Settings.Instance.BaseUrl);
            var fallbackPolicy = Policy
                .HandleResult(0)
                .Fallback(()  => throw new ApplicationException("Got nothing!"));

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
