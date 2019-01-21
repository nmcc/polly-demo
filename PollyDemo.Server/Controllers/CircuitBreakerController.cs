using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollyDemo.Server.Controllers
{
    [ApiController]
    [Route("/api/circuitbreaker")]
    public class CircuitBreakerController : ControllerBase
    {
        private static bool throwError;

        [HttpGet("{from}")]
        public ActionResult<string> SayHello(string from)
        {
            if (throwError)
                throw new ApplicationException("Throwing errors just because");

            return $"Hello {from}";
        }

        [HttpPost]
        public ActionResult<string> SwitchMode()
        {
            throwError = !throwError;

            return string.Format("Endpoint is {0}throwing errors", throwError ? string.Empty : "not ");
        }
    }
}
