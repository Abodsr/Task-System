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
        public IActionResult GetTasks()
        {
            var Tasks= _context.Tasks.ToList();
            return Ok(Tasks);

        }
        [HttpGet("SearchTask")]
        public IActionResult GetTasks(string ?Keyword)
        {
            if (Keyword == null)
            {
                return BadRequest("empty Keyword");
            }
            else 
            { 
                var Tasks = _context.Tasks.Where(x => x.Title.Contains(Keyword)).ToList();
                return Ok(Tasks);
            }


        }
        [HttpGet("{userid}")]
        public IActionResult GetTasksforUser(int userid)
        {
            var Tasks = _context.Tasks.Where(x=>x.UserID==userid).ToList();
            return Ok(Tasks);


        }
        [HttpGet("GetCompletedTasks/{userid}")]
        public IActionResult GetCompletedTasksforUser(int userid)
        {
            var Tasks = _context.Tasks.Where(x => x.UserID == userid&&x.IsCompleted==true).ToList();
            return Ok(Tasks);


        }
        [HttpPost]

        public IActionResult AddTask(UserTask task)
        {
            var user =_context.Users.FirstOrDefault(x => x.Id == task.UserID);
            if(user==null)
            {
                return BadRequest("User Not Exit");
            }
            else if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            else
            {
                Models.Task task1 = new Models.Task();
                task1.Title = task.Title;
                task1.UserID = task.UserID;
                task1.Description = task.Description;
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
            var task = _context.Tasks.FirstOrDefault(x => x.Id == TaskId);
            if (task == null)
            {
                return NotFound("Task is not Exit");
            }
            else
            {

                if (task1.Title != null)
                    task.Title = task1.Title;
                if (task1.Description != null)
                    task.Description = task.Description;
                if(task1.IsCompleted.HasValue)
                    task.IsCompleted = task1.IsCompleted;
                _context.SaveChanges();
                return Ok(task);
            }
        }
    }
}
