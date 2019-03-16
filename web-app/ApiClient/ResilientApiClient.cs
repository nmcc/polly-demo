using Microsoft.Extensions.Logging;
using Polly;
using Polly.Caching.Memory;
using Polly.CircuitBreaker;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo.App.CircuitBreaker
{
    public class ResilientApiClient : IDisposable
    {
        private readonly ApiClient apiClient;
        private readonly ILogger logger;
        private readonly Policy<byte[]> sayHelloPolicy;

        private static readonly Lazy<byte[]> defaultAvatarImage =
            new Lazy<byte[]>(() => File.ReadAllBytes("fallback.png"));

        public ResilientApiClient(ApiClient apiClient, MemoryCacheProvider memoryCacheProvider, ILogger<ResilientApiClient> logger)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var circuitBreaker = Policy
                .HandleInner<HttpRequestException>()
                .OrInner<TaskCanceledException>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(10),
                    onBreak: (exception, retryIn) => OnBreak(retryIn),
                    onReset: () => OnReset());

            var getAvatarPolicy = Policy<byte[]>
                .Handle<BrokenCircuitException>()
                .OrInner<HttpRequestException>()
                .OrInner<TaskCanceledException>()
                .FallbackAsync(defaultAvatarImage.Value);

            this.sayHelloPolicy = getAvatarPolicy.WrapAsync(circuitBreaker);
        }

        private void OnBreak(TimeSpan retryIn)
            => logger.LogWarning($"Preventing connections for {retryIn.TotalSeconds} seconds");

        private void OnReset()
            => logger.LogWarning("Connection is restablished");

        public void Dispose()
        {
            this.apiClient.Dispose();
        }

        public async Task<byte[]> GetAvatarAsync(string name)
            => await sayHelloPolicy.ExecuteAsync(() => apiClient.GetAvatarAsync(name));
    }
}
