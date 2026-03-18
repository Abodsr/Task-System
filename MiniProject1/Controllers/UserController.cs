using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject1.DTO;
using MiniProject1.Models;

namespace MiniProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
         _context = context;   
        }
        [HttpGet]
        public  IActionResult GetUsers()
        {
            var users =  _context.Users.ToList();
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest("There is no tasks add new one");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.Include(x=>x.Tasks).FirstOrDefault(x=>x.Id==id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
                
        }
        [HttpPost]
        public IActionResult AddUser([FromBody] UserDto user)
        {
            if (user != null)
            {
                User user1 = new User();
                user1.Name = user.name;
            
                _context.Users.Add(user1);
                _context.SaveChanges();
                return Ok(user);
            }
            else
            {
                return BadRequest("Invalid Data");
            }
        }
        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            var olduser= _context.Users.FirstOrDefault(x => x.Id == user.Id);
            
            if (olduser!=null)
            {
                olduser.Name = user.Name;
                _context.SaveChanges(); 
                return Ok(user);
            }
            else
            {
                return BadRequest("Invalid Data");
            }
        }
        [HttpDelete("{UserId}")]
        public IActionResult DeleteUser( int UserId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == UserId);

            if (user != null)
            {
                
                _context.Users.Remove(user);
                _context.SaveChanges() ;
                return Ok();
            }
            else
            {
                return NotFound("User not Found");
            }
        }

    }
}
