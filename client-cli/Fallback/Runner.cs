using System;
using System.Threading;

namespace PollyDemo.Client.Fallback
{
    static class Runner
    {
        internal static void Run()
        {
            var apiClient = new ApiClient(Settings.Instance.BaseUrl);

            var i = 0;
            while (true)
            {
                ++i;
                
                var doubleOrNothing = apiClient.DoubleOrNothing(i);
                Console.WriteLine($"Double or nothing of {i} is {doubleOrNothing}");

                Thread.Sleep(1000);
            }
        }
    }
}
