using System;
using System.Threading;

namespace PollyDemo.Client.CircuitBreaker
{
    class Runner
    {
        internal static void Run()
        {
            var apiClient = new ApiClient(Settings.Instance.BaseUrl);

            var i = 0;

            while (true)
            {
                i++;

                try
                {
                    Console.Write($"[{i:00}] ");
                    string message = apiClient.SayHelloAsync("NetPonto").GetAwaiter().GetResult();
                    Console.WriteLine($"server said \"{message}\"");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error {ex.Message}");
                }
                Thread.Sleep(1000);
            }
        }
    }
}
