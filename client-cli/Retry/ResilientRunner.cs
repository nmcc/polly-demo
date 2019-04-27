using Polly;
using System;
using System.Net.Http;
using System.Threading;

namespace PollyDemo.Client.Retry
{
    class ResilientRunner
    {
        internal static void Run()
        {
            var apiClient = new ApiClient(Settings.Instance.BaseUrl);

            var retryPolicy = Policy
               .HandleInner<HttpRequestException>()
               .OrInner<ArgumentException>(ex => ex.ParamName == "requestUri")
               .Retry(3, (exception, count) => Console.WriteLine(" FAILED ")) ;

            // var retryPolicy = Policy
            //     .HandleInner<HttpRequestException>()
            //     .WaitAndRetry(3,
            //         retryattemp => TimeSpan.FromSeconds(Math.Pow(2, retryattemp - 1)),
            //         (exception, retryAgainIn) => Console.WriteLine($" failed --> retrying in {retryAgainIn.TotalSeconds} seconds"));

            var i = 0;
            while (true)
            {
                retryPolicy.Execute(() =>
                {
                    i++;
                    Console.Write($"[{i:00}]");
                    string message = apiClient.SayHello("Global Azure Bootcamp", i);
                    Console.WriteLine($" server said \"{message}\"");

                    Thread.Sleep(1000);
                });
            }
        }
    }
}
