using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo.Client.Retry
{
    class ApiClient : IDisposable
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

        public string SayHello(string name, int counter) 
            => httpClient.GetStringAsync($"{this.baseUrl}api/flacky/{name}?counter={counter}").Result;
    }
}
