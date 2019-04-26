using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo.Client.CircuitBreaker
{
    class ApiClient : IDisposable, IApiClient
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;

        public ApiClient(string baseUrl)
        {
            this.baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            httpClient = new HttpClient();
        }

        public void Dispose()
        {
            this.httpClient.Dispose();
        }

        public string SayHello(string name) 
            => httpClient.GetStringAsync($"{this.baseUrl}/api/sayhello/{name}").Result;
    }
}
