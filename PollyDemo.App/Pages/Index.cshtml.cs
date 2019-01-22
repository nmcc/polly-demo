using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PollyDemo.App.CircuitBreaker;

namespace PollyDemo.App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiClient apiClient;

        public IndexModel(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public void OnGet()
        {
            var name = "NetPonto";

            this.ViewData.Add("Name", this.apiClient.SayHello(name));
        }
    }
}
