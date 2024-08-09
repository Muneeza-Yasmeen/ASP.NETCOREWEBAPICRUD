using ASP.NETCOREWEBAPICRUD.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCOREWEBAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //ControllerBase:: Ye class common functionalities aur helper methods offer karti hai jo API controllers ke liye zaroori hoti hain.
    //Features and Methods Provided by ControllerBase
    //    ActionResult Helpers:

    //Ok() : 200 OK response return karta hai.
    //BadRequest(): 400 Bad Request response return karta hai.
    //NotFound(): 404 Not Found response return karta hai.
    //Created(): 201 Created response return karta hai.
    //NoContent(): 204 No Content response return karta hai.

    
    public class UsersAPIController : ControllerBase 
    {
        private readonly UsersDbContext _context;
        private readonly ILogger<UsersAPIController> _logger;

        public UsersAPIController(UsersDbContext context, ILogger<UsersAPIController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetUsers()
        {
            _logger.LogInformation("Fetching all users");
            var data = await _context.User.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<Users>> GetUsersbyid(string name)
        {
            var data = await _context.User.FindAsync(name);
            if(data != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();  
            }
        }
     
        [HttpPost]
        public ActionResult createuser(Users user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{name}")]
        public async Task<ActionResult<Users>> Updateuser(string name,Users user)
        {
            if(name!=user.Name)
            {
                return BadRequest();    
            }
           var data=await _context.User.FindAsync(name);

            data.Name= user.Name;
            data.Email= user.Email;
            data.Password= user.Password;
            data.Address= user.Address; 
            _context.SaveChanges();
            return Ok(data);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Deleteuser(string name)
        {
            if (name ==null)
            {
                return BadRequest();
            }
            var data = await _context.User.FindAsync(name);
             _context.User.Remove(data);

           await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
