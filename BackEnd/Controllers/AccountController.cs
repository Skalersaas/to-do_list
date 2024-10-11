using Microsoft.AspNetCore.Mvc;
using To_Do_List.Data;
using To_Do_List.Models;
using static To_Do_List.Helpers.PasswordHasher;


namespace To_Do_List.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(ApplicationContext context) : Controller
    {
        [HttpGet("restart")]
        public JsonResult Restart()
        {
            context.Restart();
            return new("Restarted");
        }
        [HttpPost("login")]
        public ObjectResult Login([FromForm] string Login, [FromForm] string Password)
        {
            User? user = context.GetUser(Login);
            if (user == null || !VerifyPassword(Password, user.Password))
                return NotFound(new { response = "Wrong Login/Password" });

            return Ok(new
            {
                access_token = AuthOptions.GetJWTToken(Login, user.Id),
                username = Login,
                fullName = user.FullName
            });
        }
        [HttpPost("register")]
        public ObjectResult Register([FromForm] string Login, [FromForm] string fullName, [FromForm] string Password)
        {
            Console.WriteLine(Password);
            if (context.UserExists(Login))
                return Conflict(new { response = "User exists" });

            string hashPassword = HashPassword(Password);
            User user = new(Login, fullName, hashPassword);

            context.Users.Add(user);
            context.SaveChanges();

            context.Groups.Add(new Group("Tasks", user.Id));
            context.SaveChanges();

            return Accepted();
        }
    }
}
