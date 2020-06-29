using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Entities;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;

        public StudentController(IUserService userService, IStudentService studentService)
        {
            _userService = userService;
            _studentService = studentService;
        }

        // POST: api/student/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<StudentRegisterResponse>> Register([FromBody] StudentRegisterRequest request)
        {
            try
            {
                var newUser = await _userService.CreateAsync(request.Email, request.Password);
                var response = await _studentService.CreateAsync(request, newUser);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //// GET: api/Student
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        //{
        //    return await _context.Students.ToListAsync();
        //}

        //// GET: api/Student/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Student>> GetStudent(int id)
        //{
        //    var student = await _context.Students.FindAsync(id);

        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return student;
        //}

        //// PUT: api/Student/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStudent(int id, Student student)
        //{
        //    if (id != student.StudentId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(student).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StudentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Student
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Student>> PostStudent(Student student)
        //{
        //    _context.Students.Add(student);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        //}

        //// DELETE: api/Student/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Student>> DeleteStudent(int id)
        //{
        //    var student = await _context.Students.FindAsync(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Students.Remove(student);
        //    await _context.SaveChangesAsync();

        //    return student;
        //}

        //private bool StudentExists(int id)
        //{
        //    return _context.Students.Any(e => e.StudentId == id);
        //}
    }
}
