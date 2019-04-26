using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System;
using System.Net.Http;

namespace PollyDemo.App.Pages
{
    public class Resilient2Model : PageModel
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<Resilient2Model> logger;

        public Resilient2Model(IHttpClientFactory httpClientFactory, ILogger<Resilient2Model> logger)
        {
            this.httpClient = httpClientFactory.CreateClient("PollyDemo.Server");
            this.logger = logger;
        }

        public void OnGet()
        {
            var nameFromQuery = Request.Query["name"].ToArray();
            var name = nameFromQuery.Length > 0 ? nameFromQuery[0] : Constants.DefaultUser;

            this.ViewData.Add("Name", name);

            this.ViewData.Add("AvatarBase64", GetAvatar(name));
        }

        private string GetAvatar(string name)
        {
            try
            {
                var avatarImage = this.httpClient.GetByteArrayAsync($"/api/avatar/{name}").Result;
                return Convert.ToBase64String(avatarImage);
            }
            catch (Exception ex)
            {
                if(!(ex is BrokenCircuitException))
                {
                    logger.LogWarning("Error getting avatar", ex);
                }

                return Convert.ToBase64String(System.IO.File.ReadAllBytes("fallback.png"));
            }
        }
    }
}
