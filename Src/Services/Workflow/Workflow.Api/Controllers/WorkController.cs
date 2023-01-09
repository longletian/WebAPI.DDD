using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Workflow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkController : ControllerBase
    {
        public IActionResult GetOk()
        {
            Log.Error("≤‚ ‘»’÷æ");
            return Ok();
        }
    }
}