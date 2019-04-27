using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PollyDemo.App.CircuitBreaker;

namespace web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly PollyDemo.App.CircuitBreaker.ResilientApiClient apiClient;

        public HealthController(ResilientApiClient apiClient)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public IActionResult HealthCheck()
        {
            return new OkResult();
        }

        [Route("/api/[Controller]/deps")]
        public IActionResult HealthCheckWithDeps()
        {
            // no deps
            var result = new
            {
                WebApi = new 
                { 
                    Healthy= apiClient.IsHealthy,
                    CircuitClosed = apiClient.IsCircuitClosed
                }
            };
            return new JsonResult(result);
        }
    }
}