using Microsoft.AspNetCore.Mvc;

namespace PollyDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoubleOrNothingController : ControllerBase
    {
        [HttpGet("{number}")]
        public ActionResult<int> DoubleOrNothing(int number)
        {
            return number % 3 == 0 ? 0 : number * 2;
        }
    }
}