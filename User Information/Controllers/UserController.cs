using System.Linq;
using Microsoft.AspNetCore.Mvc;
using User_Information.models;

namespace User_Information.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;
        
        public UserController(UserDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(UserVM user)
        {
            if (user.Username == "admin" && user.Password == "pass")
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("get/all/users")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("get/user/{id:int}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return Ok(user);
        }

        [HttpPost("SignUp")]
        public IActionResult AddUser([FromBody] UserVM newUser)
        {
            var user = new User()
            {
                Username = newUser.Username,
                Password = newUser.Password
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}