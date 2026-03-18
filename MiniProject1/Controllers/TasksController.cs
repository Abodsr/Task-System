using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject1.DTO;
using MiniProject1.Models;
using System.Threading.Tasks;

namespace MiniProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TasksController(AppDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public List<Models.Task> GetTasks()
        {
            var Tasks= _context.Tasks.ToList();
            return Tasks;

        }
        [HttpGet("SearchTask/{Keyword}")]
        public List<Models.Task> GetTasks(string Keyword)
        {
            var Tasks = _context.Tasks.Where(x=>x.Title.Contains(Keyword)).ToList();
            
                return Tasks;
            

        }
        [HttpGet("{userid}")]
        public List<Models.Task> GetTasksforUser(int userid)
        {
            var Tasks = _context.Tasks.Where(x=>x.UserID==userid).ToList();
            return Tasks;

        }
        [HttpGet("GetCompletedTasks/{userid}")]
        public List<Models.Task> GetCompletedTasksforUser(int userid)
        {
            var Tasks = _context.Tasks.Where(x => x.UserID == userid&&x.IsCompleted==true).ToList();
            return Tasks;

        }
        [HttpPost("{UserId}")]

        public IActionResult AddTask(int UserId,UserTask task)
        {
            var user =_context.Users.Where(x=>x.Id==UserId).FirstOrDefault();
            if(user==null)
            {
                return BadRequest("User Not Exit");
            }
            else
            {
                Models.Task task1 = new Models.Task();
                task1.Title =task.Title;    
                task1.Description =task.Description;
                task1.IsCompleted = task.IsCompleted;
                user.Tasks.Add(task1);
                _context.SaveChanges();
                return Ok(task1);
            }
        }
        [HttpDelete("{TaskId}")]
        public IActionResult DeleteTask(int TaskId)
        {
            var task= _context.Tasks.FirstOrDefault(x=>x.Id== TaskId);
            if(task!=null)
            {
                _context.Remove(task);
                _context.SaveChanges();
                return Ok(task);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{TaskId}")]

        public IActionResult UpdateTask(int TaskId,UserTask task1)
        {
            var task = _context.Tasks.Where(x => x.Id == TaskId).FirstOrDefault();
            if (task == null)
            {
                return NotFound("Task is not Exit");
            }
            else
            {
                
                task.Title = task1.Title;
                task.Description = task1.Description;
                task.IsCompleted = task1.IsCompleted;
                _context.SaveChanges();
                return Ok(task);
            }
        }
    }
}
