﻿using System;
using System.Threading;

namespace PollyDemo.Client.Retry
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
                var message = apiClient.SayHello("Global Azure Bootcamp", i);
                Console.WriteLine($"[{i:00}] server said \"{message}\"");

                Thread.Sleep(1000);
            }
        }
    }
}
