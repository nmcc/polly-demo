using System;
using System.Threading;

namespace PollyDemo.Client.Cache
{
    static class Runner
    {
        internal static void Run()
        {
            var apiClient = new ApiClient(Settings.Instance.BaseUrl);

            var i = 0;
            const string name = "NetPonto";

            while (true)
            {
                i++;

                var message = apiClient.SayHello(name);
                Console.WriteLine($"[{i:00}] server said \"{message}\"");

                Thread.Sleep(1000);
            }
        }
    }
}
