using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace PollyDemo.App.CircuitBreaker
{
    public class ApiClient : IDisposable
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;

        public ApiClient(IConfiguration configurationRoot)
        {
            this.baseUrl = configurationRoot.GetSection("Server:BaseUrl").Value;

            this.httpClient = new HttpClient();
        }

        public void Dispose()
        {
            this.httpClient.Dispose();
        }

        public string SayHello(string name) 
            => httpClient.GetStringAsync($"{this.baseUrl}api/circuitbreaker/{name}").Result;
    }
}
