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
        
        // POST: api/students
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<StudentResponse>> Post([FromBody] StudentRequest request)
        {
            var response = await _studentService.CreateAsync(request);
            return StatusCode(201, response);

        }

        // PUT: api/students/1
        [Authorize(Roles = Role.Operator)]
        [HttpPut("{studentId}")]
        public async Task<IActionResult> Put(
            [FromRoute] int studentId,
            [FromBody] StudentRoleRequest request)
        {
            bool response = await _studentService.ChangeRoleAsync(studentId, request);
            if (!response)
            {
                return StatusCode(404);
            }
            return StatusCode(204);
        }
    }
}
