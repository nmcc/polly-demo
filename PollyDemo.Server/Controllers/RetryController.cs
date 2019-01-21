using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;

namespace PollyDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetryController : ControllerBase
    {
        [HttpGet("{from}")]
        public ActionResult<string> SayHello(string from, [FromQuery] int counter)
        {
            if (counter % 5 == 3 || counter % 5 == 4)
            {
                throw new ApplicationException("Refuse to return a result every 3rd and 4th request");
            }

            return $"Hello {from}";
        }
    }
}