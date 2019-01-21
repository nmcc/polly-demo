using Polly;
using Polly.CircuitBreaker;
using System;
using System.Net.Http;

namespace PollyDemo.Client.CircuitBreaker
{
    class ResilientApiClient : IDisposable, IApiClient
    {
        private readonly ApiClient apiClient;

        private readonly CircuitBreakerPolicy circuitBreaker;

        public ResilientApiClient(string baseUrl)
        {
            this.apiClient = new ApiClient(baseUrl);

            this.circuitBreaker = Policy
                .HandleInner<HttpRequestException>()
                .CircuitBreaker(
                    exceptionsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(5),
                    onBreak: (exception, retryIn) => OnBreak(retryIn),
                    onReset: () => OnReset());
        }

        private void OnBreak(TimeSpan retryIn)
            => Console.WriteLine($"Preventing connections for {retryIn.TotalSeconds} seconds");

        private void OnReset()
            => Console.WriteLine($"Connection is restablished");

        public void Dispose()
        {
            this.apiClient.Dispose();
        }

        public string SayHello(string name) => circuitBreaker.Execute(() => apiClient.SayHello(name));
    }
}
