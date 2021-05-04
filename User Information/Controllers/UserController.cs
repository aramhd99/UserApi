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
            var _user = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (_user != null && _user.Password == user.Password)
            {
                return Ok("welcome");
            }

            return NotFound("something went wrong");
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

        [HttpPut("update/user/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserVM User)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            user.Username = User.Username;
            user.Password = User.Password;
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpDelete("delete/user/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}