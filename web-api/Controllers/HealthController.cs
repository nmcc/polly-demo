using Microsoft.AspNetCore.Mvc;

namespace PollyDemo.Server.Controllers
{
    [Route("/api/health")]
    [ApiController]
    public class HealthController
    {
        public IActionResult HealthCheck()
        {
            return new OkResult();
        }

        [Route("/api/health/deps")]
        public IActionResult HealthCheckWithDeps()
        {
            // no deps
            var result = new
            {
            };
            return new JsonResult(result);
        }
    }
}
