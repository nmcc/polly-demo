using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo.App.CircuitBreaker
{
    public class ApiClient : IDisposable
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;

        public ApiClient(IConfiguration configurationRoot)
        {
            this.baseUrl = configurationRoot.GetSection("Server:BaseUrl").Value;

            this.httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(1)
            };
        }

        public void Dispose()
        {
            this.httpClient.Dispose();
        }

        public async Task<byte[]> GetAvatarAsync(string name) 
            => await httpClient.GetByteArrayAsync($"{this.baseUrl}api/avatar/{name}");
    }
}
