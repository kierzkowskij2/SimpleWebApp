using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleWebApp.Models;

namespace SimpleWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UsersContext _usersContext;

        public UsersController(ILogger<UsersController> logger, UsersContext usersContext)
        {
            _logger = logger;
            _usersContext = usersContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _usersContext.Users.ToListAsync();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _usersContext.Users.FindAsync((long)id);

            if (user == null)
            {
                return new NotFoundResult();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _usersContext.Add(user);
            await _usersContext.SaveChangesAsync();

            return CreatedAtAction("CreateUser", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _usersContext.Users.FindAsync((long)id);

            if (user == null)
            {
                return BadRequest();
            }

            _usersContext.Users.Remove(user);
            await _usersContext.SaveChangesAsync();

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (!_usersContext.Users.Any(x => x.Id == (long)id))
            {
                return NotFound();
            }

            if ((long)id == user.Id)
            {
                return BadRequest();
            }

            _usersContext.Entry(user).State = EntityState.Modified;
            await _usersContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}
