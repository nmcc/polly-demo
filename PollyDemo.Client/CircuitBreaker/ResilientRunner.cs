using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PollyDemo.Client.CircuitBreaker
{
    class ResilientRunner
    {
        internal static void Run()
        {
            var apiClient = new ResilientApiClient(Settings.Instance.BaseUrl);

            var i = 0;

            while (true)
            {
                i++;

                try
                {
                    Console.Write($"[{i:00}] ");
                    string message = apiClient.SayHello("NetPonto");
                    Console.WriteLine($"server said \"{message}\"");
                }
                catch (BrokenCircuitException)
                {
                    Console.WriteLine($"Not called because circuit is broken");
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
