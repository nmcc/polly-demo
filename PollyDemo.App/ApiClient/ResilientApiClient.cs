using Microsoft.Extensions.Logging;
using Polly;
using Polly.Caching.Memory;
using Polly.CircuitBreaker;
using Polly.Wrap;
using System;
using System.Net.Http;

namespace PollyDemo.App.CircuitBreaker
{
    public class ResilientApiClient : IDisposable
    {
        private readonly ApiClient apiClient;
        private readonly ILogger logger;
        private readonly Policy<string> sayHelloPolicy;

        public ResilientApiClient(ApiClient apiClient, MemoryCacheProvider memoryCacheProvider, ILogger<ResilientApiClient> logger)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var circuitBreaker = Policy
                .HandleInner<HttpRequestException>()
                .CircuitBreaker(
                    exceptionsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(5),
                    onBreak: (exception, retryIn) => OnBreak(retryIn),
                    onReset: () => OnReset());

            var sayHelloFallback = Policy<string>
                .Handle<BrokenCircuitException>()
                .OrInner<HttpRequestException>()
                .Fallback("FALLBACK");

            this.sayHelloPolicy = sayHelloFallback.Wrap(circuitBreaker);
        }

        private void OnBreak(TimeSpan retryIn)
            => logger.LogWarning($"Preventing connections for {retryIn.TotalSeconds} seconds");

        private void OnReset()
            => logger.LogWarning("Connection is restablished");

        public void Dispose()
        {
            this.apiClient.Dispose();
        }

        public string SayHello(string name) => sayHelloPolicy.Execute(() => apiClient.SayHello(name));
    }
}
