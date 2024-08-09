using ASP.NETCOREWEBAPICRUD.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCOREWEBAPICRUD.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogger<UsersAPIController> _logger;

        public LogController(UsersDbContext context, ILogger<UsersAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("This is log message. This is an object: {User}", new { Name = "John" });
             return Ok();
        }
    }
}
