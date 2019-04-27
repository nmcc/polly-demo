using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo.App.CircuitBreaker
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        /* Parameterless ctor is here for the scenario  where this ApiClient is used within Polly's 
         * Http client extensions: Polly.Extensions.Http
         */
        public ApiClient()
        {
        }

        public ApiClient(IConfiguration configurationRoot)
        {
            this.BaseUrl = configurationRoot.GetSection("Server:BaseUrl").Value;

            this.httpClient = new HttpClient();

            // Uncomment to set a timeout on the HttpClient class
            //this.httpClient.Timeout = TimeSpan.FromSeconds(1);
        }

        public string BaseUrl { get; set; }

        public void Dispose() => this.httpClient.Dispose();

        public async Task<byte[]> GetAvatarAsync(string name)
            => await httpClient.GetByteArrayAsync($"{this.BaseUrl}/api/avatar/{name}");

        public bool IsHealthy
        {
            get
            {
                try
                {
                    var s = httpClient.GetStringAsync($"{this.BaseUrl}/api/health").Result;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsCircuitClosed
            => throw new NotImplementedException("Non-resilient client does not support Circuit breaker");
    }
}
