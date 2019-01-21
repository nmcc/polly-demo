using System;
using System.Collections.Generic;
using System.Text;

namespace PollyDemo.Client.Retry
{
    class Runner
    {
        internal static void Run(int iterations)
        {
            var apiClient = new ApiClient(Settings.Instance.BaseUrl);

            for (int i = 0; i < iterations; i++)
            {
                string message = apiClient.SayHello("NetPonto", i);
                Console.WriteLine($"[{i:00}] server said \"{message}\"");
            }
        }
    }
}
