using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Threading;

namespace PollyDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private static bool throwError;
        private static int? delaySeconds;

        [HttpGet("{username}")]
        public IActionResult GetAvatar(string username)
        {
            if (throwError)
                return StatusCode(500);

            if (delaySeconds.HasValue)
                Thread.Sleep(delaySeconds.Value * 1000);

            using (var bitmap = new Bitmap(50, 50))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);
                    using (Brush b = new SolidBrush(ColorTranslator.FromHtml("#5e2b97")))
                    {

                        g.FillEllipse(b, 0, 0, 49, 49);
                    }

                    float emSize = 12;
                    var shortUsername = GetShortUsername(username);
                    g.DrawString(shortUsername,
                        new Font(FontFamily.GenericSansSerif, emSize, FontStyle.Regular),
                        new SolidBrush(Color.White), 10, 15);
                }

                using (var memStream = new System.IO.MemoryStream())
                {
                    bitmap.Save(memStream, System.Drawing.Imaging.ImageFormat.Png);
                    var result = this.File(memStream.GetBuffer(), "image/png");
                    return result;
                }
            }
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
            const int defaultDelay = 10;
            delaySeconds = delaySeconds.HasValue ? (int?)null : defaultDelay;
            return string.Format(
                "Endpoint is {0}",
                delaySeconds.HasValue ? $"delayed by {defaultDelay} seconds" : "responding immediately");
        }

        private string GetShortUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return "??";

            var words = username.Split(" ");

            if (words.Length == 1)
            {
                return username.Substring(0, 2).ToUpper();
            }

            return string.Concat(words[0].Substring(0, 1), words[words.Length - 1].Substring(0, 1))
                .ToUpper();
        }
    }
}
