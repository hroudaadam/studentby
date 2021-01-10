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
    /// Controller for JobApplication
    /// </summary>
    [Route("api/job-applications")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        // GET: api/job-applications
        [HttpGet]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<IEnumerable<JobApplicationWithSimpleJoRes>>> GetList()
        {
            var response = await _jobApplicationService.GetListAsync();
            return StatusCode(200, response);
        }

        // GET: api/job-applications
        [HttpGet]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<ActionResult<IEnumerable<JobApplicationWithSimpleJoRes>>> GetListAsStudent()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            var response = await _jobApplicationService.GetListStudentAsync(userId);
            return StatusCode(200, response);
        }

        // GET: api/job-applications/1
        [HttpGet("{jobApplicationId}")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<JobApplicationWithJoStudRes>> Get([FromRoute] int jobApplicationId)
        {
            var response = await _jobApplicationService.GetDetailOperatorAsync(jobApplicationId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // GET: api/job-applications/1/student
        [HttpGet("{jobApplicationId}/student")]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<ActionResult<JobApplicationWithJoRes>> GetAsStudent([FromRoute] int jobApplicationId)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            var response = await _jobApplicationService.GetDetailStudentAsync(jobApplicationId, userId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // POST: api/job-applications
        [HttpPost]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<ActionResult<JobApplicationRes>> Post([FromBody] JobApplicationWithJoReq request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobApplicationService.CreateAsync(request, userId);
            return StatusCode(201, response);
        }

        // PUT: api/job-applications/1
        [HttpPut("{jobApplicationId}")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<IActionResult> Put(
            [FromRoute] int jobApplicationId,
            [FromBody] JobApplicationWithStateReq request)
        {
            bool found = await _jobApplicationService.EditAsync(jobApplicationId, request);
            if (!found)
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
            if (!found)
            {
                return StatusCode(404);
            }
            return StatusCode(204);
        }
    }
}
