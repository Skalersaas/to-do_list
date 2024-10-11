using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using To_Do_List.Data;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    [Route("group")]
    [ApiController]
    public class GroupController(ApplicationContext context) : Controller
    {
        [HttpPost("create")]
        [Authorize]
        public ActionResult Create([FromForm] string title)
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");
            Models.Group group = new(title, userId);
            context.Groups.Add(group);
            context.SaveChanges();

            return Created("", new { group = new GroupDTO(group) });
        }
        [HttpPost("rename")]
        [Authorize]
        public ActionResult Rename([FromForm] int groupId, [FromForm] string newTitle)
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");

            var group = context.Groups.FirstOrDefault(x => x.Id == groupId);
            if (group == null)
                return NotFound();
            group.Name = newTitle;
            context.Groups.Update(group);
            context.SaveChanges();
            return Ok(new GroupDTO(group));
        }
        [HttpPost]
        [Authorize]
        public ActionResult Remove([FromForm] int groupId)
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");

            Models.Group? group = context.Groups.FirstOrDefault(g => g.Id == groupId);
            if (group == null)
                return NotFound(new { response = "Group not found" });
            context.Groups.Remove(group);
            context.SaveChanges();
            return Ok();
        }
        [HttpGet("get")]
        [Authorize]
        public ActionResult GetGroups()
        {
            if (!int.TryParse(User.FindFirstValue("Id"), out int userId))
                return BadRequest("Incorrect jwt");
            var groups = context.Groups.Include(g => g.Tasks).Where(g => g.UserId == userId).Select(g => new GroupDTO(g));
            return Ok(groups.ToList());
        }
    }
}
