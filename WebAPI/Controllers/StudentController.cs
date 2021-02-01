using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for Student
    /// </summary>
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(
            IStudentService studentService
            )
        {
            _studentService = studentService;
        }

        // GET: api/students
        [Authorize(Roles = UserRoles.Operator)]
        [HttpGet]
        public async Task<ActionResult<StudentMinRes>> GetList()
        {
            var response = await _studentService.GetListAsync();
            return StatusCode(200, response);

        }

        // GET: api/students/profile
        [Authorize(Roles = UserRoles.Student + "," + UserRoles.StudentInact)]
        [HttpGet("profile")]
        public async Task<ActionResult<StudentWithAdActivRes>> GetProfile()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            int studentId = await _studentService.GetStudentIdAsync(userId);
            var response = await _studentService.GetDetailAsync(studentId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // GET: api/students/1
        [Authorize(Roles = UserRoles.Operator)]
        [HttpGet("{studentId}")]
        public async Task<ActionResult<StudentWithAdActivRes>> Get([FromRoute] int studentId)
        {
            var response = await _studentService.GetDetailAsync(studentId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }


        // POST: api/students
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<StudentMinRes>> Post([FromBody] StudentReq request)
        {
            var response = await _studentService.CreateAsync(request);
            return StatusCode(201, response);
        }

        // PUT: api/students/1
        [Authorize(Roles = UserRoles.Operator)]
        [HttpPut("{studentId}")]
        public async Task<IActionResult> Put(
            [FromRoute] int studentId,
            [FromBody] StudentWithRoleReq request)
        {
            bool found = await _studentService.EditOperatorAsync(studentId, request);
            if (!found)
            {
                return StatusCode(404);
            }
            return StatusCode(204);
        }
    }
}
