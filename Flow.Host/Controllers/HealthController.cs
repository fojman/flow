using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Flow.Controllers
{
    [ApiController]
    [Route("")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("/health")]
        public Task<IActionResult> Get()
        {
            using (_logger.BeginScope(new[]
            {
                ("Session", HttpContext.TraceIdentifier)
            }))
            {
                _logger.LogInformation($">> some health {HttpContext.TraceIdentifier}");
                return Task.FromResult<IActionResult>(new ObjectResult(new {ThreadId = Thread.CurrentThread.ManagedThreadId}));
            }
        }
        
    }
}