using Microsoft.AspNetCore.Mvc.RazorPages;
using PollyDemo.App.CircuitBreaker;
using System;

namespace PollyDemo.App.Pages
{
    public class NaiveModel : PageModel
    {
        private readonly ApiClient apiClient;

        public NaiveModel(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public void OnGet()
        {
            var nameFromQuery = Request.Query["name"].ToArray();
            var name =  nameFromQuery.Length > 0 ? nameFromQuery[0] : Constants.DefaultUser;
            
            this.ViewData.Add("Name", name);

            var avatarImage = this.apiClient.GetAvatarAsync(name).Result;
            this.ViewData.Add("AvatarBase64", Convert.ToBase64String(avatarImage));
        }
}
}
