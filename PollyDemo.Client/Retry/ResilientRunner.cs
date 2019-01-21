using Polly;
using System;
using System.Net.Http;

namespace PollyDemo.Client.Retry
{
    class ResilientRunner
    {
        internal static void Run(int iterations)
        {
            var apiClient = new ApiClient(Settings.Instance.BaseUrl);

            //var retryPolicy = Policy
            //    .HandleInner<HttpRequestException>()
            //    .OrInner<ArgumentException>(ex => ex.ParamName == "requestUri")
            //    .Retry(3, (exception, count) => Console.WriteLine()) ;

            var retryPolicy = Policy
                .HandleInner<HttpRequestException>()
                .WaitAndRetry(3,
                    retryattemp => TimeSpan.FromSeconds(Math.Pow(2, retryattemp)),
                    (exception, retryAgainIn) => Console.WriteLine($" failed --> retrying in {retryAgainIn.TotalSeconds} seconds"));

            for (int i = 0; i < iterations;)
            {
                retryPolicy.Execute(() =>
                {
                    Console.Write($"[{i:00}]");
                    string message = apiClient.SayHello("NetPonto", i++);
                    Console.WriteLine($"server said \"{message}\"");
                });
            }
        }
    }
}
