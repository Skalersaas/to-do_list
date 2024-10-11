using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using To_Do_List.Data;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    [Route("task")]
    [ApiController]
    public class TaskController(ApplicationContext context) : Controller
    {
        [HttpPost("create")]
        [Authorize]
        public ActionResult Create([FromForm] string title, [FromForm] int groupId, [FromForm] bool myDay)
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");

            Models.Task task = new(title, userId, groupId);
            task.MyDay = myDay;

            context.Tasks.Add(task);
                context.SaveChanges();
            return Created($"get/{task.Id}", new TaskDTO(task));
        }
        [HttpPost("delete")]
        [Authorize]
        public ActionResult Delete([FromForm] int taskId)
        {
            Models.Task? task = context.Tasks.FirstOrDefault(t => t.Id ==  taskId);
            if (task == null)
                return NotFound(new { response = "No task with this id" });

            context.Tasks.Remove(task);
            context.SaveChanges();

            return Ok(new { response = $"Deleted task with id = {taskId}" });
        }
        [Authorize]
        [HttpPost("rename")]
        public ActionResult Rename([FromForm] int taskId, [FromForm] string newTitle)
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");

            var task = context.Tasks.FirstOrDefault(x => x.Id == taskId);
            if (task == null)
                return NotFound();
            task.Title = newTitle;
            context.Tasks.Update(task);
            context.SaveChanges();
            return Ok(new TaskDTO(task));

        }
        [Authorize]
        [HttpPatch("patch")]
        public ActionResult Patch(TaskPatch task)
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");
            var found = context.Tasks.FirstOrDefault(t => t.Id == task.Id);
            if (found == null)
                return NotFound();
            found.Title = task.Title;
            found.DueTo = task.DueTo;

            if (!found.IsCompleted && task.IsCompleted)
            {
                found.CompletedOn = DateTime.Now.ToUniversalTime();
                if (found.RepeatType != RepeatType.None)
                {
                    var repeated = Repeat(found);
                    context.Tasks.Add(repeated);
                }
            }
            found.IsCompleted = task.IsCompleted;
            found.MyDay = task.MyDay;
            if (found.RepeatType == 0 && task.RepeatType != 0 && found.DueTo == DateTime.MinValue)
            {
                found.DueTo = DateTime.Now.ToUniversalTime();
            }
            found.RepeatType = task.RepeatType;
            context.SaveChanges();

            return Ok(found);
        }
        private static Models.Task Repeat(Models.Task init)
        {
            return new(init.Title, init.UserId, init.GroupId)
            {
                MyDay = init.MyDay,
                RepeatType = init.RepeatType,
                DueTo = init.DueTo.AddDays(
                        init.RepeatType switch
                        {
                            RepeatType.Daily => 1,
                            RepeatType.Weekly => 7,
                            RepeatType.Monthly => 31,
                            RepeatType.Yearly => 365,
                            _ => 0
                        })
            };
        }
        [Authorize]
        [HttpPost("getgroup")]
        public ActionResult GetTasksByGroup([FromForm] int groupId)
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");
            return Ok(context.Tasks.Where(t => t.GroupId == groupId).Select(t => new TaskDTO(t)).ToList());
        }

        [Authorize]
        [HttpGet("baseCount")]
        public ActionResult GetBaseTaskCount()
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");
            var tasks = context.Tasks.Where(x => x.UserId == userId);
            int MyDay = tasks.Count(t => t.MyDay);
            int Planned = tasks.Count(x => !x.IsCompleted);
            int Completed = tasks.Count(x => x.IsCompleted);
            return Ok(new int[3] { MyDay, Planned, Completed });
        }
        [Authorize]
        [HttpGet("myday")]
        public ActionResult MyDayTasks()
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");
            return Ok(context.Tasks.Where(x => x.UserId == userId && x.MyDay).Select(t => new TaskDTO(t)).ToList());
        }
        [Authorize]
        [HttpGet("planned")]
        public ActionResult PlannedTasks()
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");
            return Ok(context.Tasks.Where(x => x.UserId == userId && !x.IsCompleted).Select(t => new TaskDTO(t)).ToList());
        }
        [Authorize]
        [HttpGet("completed")]
        public ActionResult CompletedTasks()
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");
            return Ok(context.Tasks.Where(x => x.UserId == userId && x.IsCompleted).Select(t => new TaskDTO(t)).ToList());
        }
    }
}
