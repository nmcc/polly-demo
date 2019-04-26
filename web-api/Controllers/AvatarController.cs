using Microsoft.AspNetCore.Mvc;
using PollyDemo.Server.Avatar;
using System;
using System.Drawing;
using System.Net.Http.Headers;
using System.Threading;

namespace PollyDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private static bool throwError;
        private static int? delaySeconds;

        private readonly IAvatarProvider avatarProvider;

        public AvatarController(IAvatarProvider avatarProvider)
        {
            this.avatarProvider = avatarProvider ?? throw new ArgumentNullException(nameof(avatarProvider));
        }

        [HttpGet("{username}")]
        public IActionResult GetAvatar(string username)
        {
            if (throwError)
                return StatusCode(500);

            if (delaySeconds.HasValue)
                Thread.Sleep(delaySeconds.Value * 1000);

            var stream = this.avatarProvider.GetAvatarForUsername(username);

            if (stream.Length == 0)
            {
                return StatusCode(404);
            }

            return File(stream, "image/png");
        }

        [HttpPost]
        [Route("error")]
        public ActionResult<string> SwitchErrorMode()
        {
            throwError = !throwError;
            return string.Format("Endpoint is {0}throwing errors", throwError ? string.Empty : "not ");

        }

        [HttpPost]
        [Route("delay")]
        public ActionResult<string> SwitchDelayMode()
        {
            const int defaultDelay = 5;
            delaySeconds = delaySeconds.HasValue ? (int?)null : defaultDelay;
            return string.Format(
                "Endpoint is {0}",
                delaySeconds.HasValue ? $"delayed by {defaultDelay} seconds" : "responding immediately");
        }
    }
}
