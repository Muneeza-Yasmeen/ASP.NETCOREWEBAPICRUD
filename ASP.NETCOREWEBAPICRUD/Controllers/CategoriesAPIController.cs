using ASP.NETCOREWEBAPICRUD.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASP.NETCOREWEBAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesAPIController : Controller
    {
        private readonly UsersDbContext _context;

        public CategoriesAPIController(UsersDbContext Context)
        {
            _context = Context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Categories>>> GetTasks()
        {
            var data = await _context.Category.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{CategoryId}")]
        public async Task<ActionResult<Categories>> GetUsersbyname(int CategoryId)
        {
            var data = await _context.Category.FindAsync(CategoryId);
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
        public ActionResult createtask(Categories cate)
        {
            _context.Category.Add(cate);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{CategoryId}")]
        public async Task<ActionResult<Taskss>> Updatetask(int CategoryId, Categories cate)
        {
            if (CategoryId != cate.CategoryId)
            {
                return BadRequest();
            }
            var data = await _context.Tasks.FindAsync(CategoryId);

            data.CategoryId = cate.CategoryId;
            data.Name = cate.Name;
           
            _context.SaveChanges();
            return Ok(data);
        }

        [HttpDelete("{CategoryId}")]
        public async Task<ActionResult> Deletetask(int CategoryId)
        {
            if (CategoryId == null)
            {
                return BadRequest();
            }
            var data = await _context.Category.FindAsync(CategoryId);
            _context.Category.Remove(data);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}