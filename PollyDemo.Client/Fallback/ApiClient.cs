using System;
using System.Net.Http;

namespace PollyDemo.Client.Fallback
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

        public int DoubleOrNothing(int number) 
            => Convert.ToInt32(httpClient.GetStringAsync($"{this.baseUrl}api/fallback/{number}").Result);
    }
}
