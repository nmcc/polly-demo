using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;

namespace PollyDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlackyController : ControllerBase
    {
        [HttpGet("{from}")]
        public ActionResult<string> SayHello(string from, [FromQuery] int counter)
        {
            // Introduce flakyness 
            if (counter % 6 >= 3)
            {
                throw new ApplicationException("I refuse to attend this request");
            }

            return $"Hello {from}";
        }
    }
}