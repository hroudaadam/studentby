using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/alibaba")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly StudentbyContext _context;
        private readonly IUserService _userService;

        public TestController(StudentbyContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Test/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Test
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        // GET: api/test/adam
        [HttpGet("adam")]
        public async Task<ActionResult<ITestDTO>> TryAdam()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            string userRole = await _userService.GetUserRole(userId);

            if (userRole == UserRoles.Student)
            {
                var response = new TestDTO1();
                return StatusCode(200, response);
            }

            if (userRole == UserRoles.Operator)
            {
                var response = new TestDTO2();
                return StatusCode(200, response);
            }

            throw new Exception();
        }

        public interface ITestDTO
        {
            int TestId { get; set; }
        }

        public class TestDTO1: ITestDTO
        {
            public string TestStudent { get; set; } = "Ahoj";
            public int TestId { get; set; } = 1;
        }

        public class TestDTO2: ITestDTO
        {
            public string TestOperator { get; set; } = "Sbohem";
            public int TestId { get; set; } = 5;
            public string AddDesc { get; set; } = "Test";
        }
    }
}
