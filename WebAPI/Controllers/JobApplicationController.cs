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
    [Route("api/job-applications")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IUserService userService, IJobApplicationService jobApplicationService)
        {
            _userService = userService;
            _jobApplicationService = jobApplicationService;
        }

        // GET: api/job-applications
        [HttpGet]
        [Authorize(Roles = Role.Student + "," + Role.Operator)]
        public async Task<ActionResult<IEnumerable<JobApplicationSimpleResponse>>> GetAll()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            string userRole = await _userService.GetUserRole(userId);
            IEnumerable<JobApplicationSimpleResponse> response;

            if (userRole == Role.Student)
            {
                response = await _jobApplicationService.GetListStudentAsync(userId);
            }
            else if (userRole == Role.Operator)
            {
                response = await _jobApplicationService.GetListOperatorAsync();
            }
            else
            {
                throw new Exception();
            }

            return StatusCode(200, response);
        }

        // GET: api/job-applications/1
        [HttpGet("{jobApplicationId}")]
        [Authorize(Roles = Role.Student + "," + Role.Operator)]
        public async Task<ActionResult<IJobApplicationDetail>> Get([FromRoute] int jobApplicationId)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            string userRole = await _userService.GetUserRole(userId);
            IJobApplicationDetail response;

            if (userRole == Role.Student)
            {
                response = await _jobApplicationService.GetDetailStudentAsync(jobApplicationId, userId);
            }
            else if (userRole == Role.Operator)
            {
                response = await _jobApplicationService.GetDetailOperatorAsync(jobApplicationId);
            }
            else
            {
                throw new Exception();
            }

            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // POST: api/job-applications
        [HttpPost]
        [Authorize(Roles = Role.Student)]
        public async Task<ActionResult<JobApplicationResponse>> Post([FromBody] JobApplicationRequest request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobApplicationService.CreateAsync(request, userId);
            return StatusCode(201, response);
        }

        // PUT: api/job-applications/1
        [HttpPut("{jobApplicationId}")]
        [Authorize(Roles = Role.Operator)]
        public async Task<IActionResult> Put(
            [FromRoute] int jobApplicationId,
            [FromBody] JobApplicationStateRequest request)
        {
            bool response = await _jobApplicationService.EditStateAsync(jobApplicationId, request);
            if (!response)
            {
                return StatusCode(404);
            }
            return StatusCode(204);
        }

        // DELETE: api/job-applications/1
        [HttpDelete("{jobApplicationId}")]
        public async Task<ActionResult> Delete([FromRoute] int jobApplicationId)
        {
            bool found = await _jobApplicationService.DeleteAsync(jobApplicationId);
            if (found)
            {
                return StatusCode(204);
            }
            return StatusCode(404);
        }
    }
}
