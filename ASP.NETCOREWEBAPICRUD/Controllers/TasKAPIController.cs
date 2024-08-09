/*using ASP.NETCOREWEBAPICRUD.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASP.NETCOREWEBAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasKAPIController : Controller
    {
        private readonly UsersDbContext _context;

        public TasKAPIController(UsersDbContext Context)
        {
            _context = Context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Taskss>>> GetTasks()
        {
            var data = await _context.Tasks.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{TaskId}")]
        public async Task<ActionResult<Taskss>> GetUsersbyname(int taskid)
        {
            var data = await _context.Tasks.FindAsync(taskid);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }
        //[HttpPost]
        *//*  public ActionResult createtask(Taskss tasek)
          {
              _context.Tasks.Add(tasek);
              _context.SaveChanges();
              return Ok();
          }*//*
        [HttpPost]
        public ActionResult CreateTask(Taskss tasek)
        {
            // Handle user insertion or retrieval if needed
            var user = _context.User.SingleOrDefault(u => u.Name == tasek.User.Name);
            if (user == null)
            {
                _context.User.Add(tasek.User);
            }
            else
            {
                tasek.User = user;
            }

            // Do not handle category insertion if it already exists
            var category = _context.Category.SingleOrDefault(c => c.CategoryId == tasek.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid category ID");
            }

            _context.Tasks.Add(tasek);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{TaskId}")]
        public async Task<ActionResult<Taskss>> Updatetask(int TaskId, Taskss tasek)
        {
            if (TaskId != tasek.TaskId)
            {
                return BadRequest();
            }
            var data = await _context.Tasks.FindAsync(TaskId);

            data.TaskId = tasek.TaskId;
            data.Title = tasek.Title;
            data.Description = tasek.Description;
            data.IsCompleted = tasek.IsCompleted;
            data.DueDate= tasek.DueDate;
            data.CategoryId= tasek.CategoryId;
            data.Name= tasek.Name;
            data.Priority= tasek.Priority;
            _context.SaveChanges();
            return Ok(data);
        }

        [HttpDelete("{TaskId}")]
        public async Task<ActionResult> Deletetask(int TaskId)
        {
            if (TaskId == null)
            {
                return BadRequest();
            }
            var data = await _context.Tasks.FindAsync(TaskId);
            _context.Tasks.Remove(data);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}
*/



using ASP.NETCOREWEBAPICRUD.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ASP.NETCOREWEBAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAPIController : Controller
    {
        private readonly UsersDbContext _context;
        private readonly ILogger<TaskAPIController> _logger;

        public TaskAPIController(UsersDbContext context, ILogger<TaskAPIController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Taskss>>> GetTasks()
        {
            var data = await _context.Tasks.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{TaskId}")]
        public async Task<ActionResult<Taskss>> GetUsersbyname(int taskid)
        {
            var data = await _context.Tasks.FindAsync(taskid);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult CreateTask(Taskss task)
        {
            _logger.LogInformation("Received Task: {@Task}", task);

            var user = _context.User.SingleOrDefault(u => u.Name == task.Name);
            if (user == null)
            {
                return BadRequest(new { User = "The User field is required." });
            }

            var category = _context.Category.SingleOrDefault(c => c.CategoryId == task.CategoryId);
            if (category == null)
            {
                return BadRequest(new { Category = "The Category field is required." });
            }

            task.User = user;
            task.Category = category;

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{TaskId}")]
        public async Task<ActionResult<Taskss>> Updatetask(int TaskId, Taskss task)
        {
            if (TaskId != task.TaskId)
            {
                return BadRequest();
            }

            var data = await _context.Tasks.FindAsync(TaskId);
            if (data == null)
            {
                return NotFound();
            }

            data.Title = task.Title;
            data.Description = task.Description;
            data.IsCompleted = task.IsCompleted;
            data.Priority = task.Priority;
            data.DueDate = task.DueDate;
            data.Name = task.Name;
            data.CategoryId = task.CategoryId;

            _context.SaveChanges();
            return Ok(data);
        }
        [HttpGet("user/{name}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetUserTasks(string name)
        {
            var tasks = await _context.Tasks.Where(t => t.User.Name == name).ToListAsync();

            if (tasks == null || tasks.Count == 0)
            {
                return NotFound();
            }

            return Ok(tasks);
        }
        [HttpDelete("{TaskId}")]
        public async Task<ActionResult> Deletetask(int TaskId)
        {
            var data = await _context.Tasks.FindAsync(TaskId);
            if (data == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(data);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
