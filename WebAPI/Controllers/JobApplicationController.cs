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

        // GET: api/job-applications/operator-view
        [HttpGet("operator-view")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<IEnumerable<JobApplicationMinWithJoRes>>> GetListOperator()
        {
            var response = await _jobApplicationService.GetListOperatorAsync();
            return StatusCode(200, response);
        }

        // GET: api/job-applications/student-view
        [HttpGet("student-view")]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<ActionResult<IEnumerable<JobApplicationMinWithJoRes>>> GetListStudent()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            var response = await _jobApplicationService.GetListStudentAsync(userId);
            return StatusCode(200, response);
        }

        // GET: api/job-applications/1/operator-view
        [HttpGet("{jobApplicationId}/operator-view")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<JobApplicationWithJoStudRes>> GetOperator([FromRoute] int jobApplicationId)
        {
            var response = await _jobApplicationService.GetDetailOperatorAsync(jobApplicationId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // GET: api/job-applications/1/student-view
        [HttpGet("{jobApplicationId}/student-view")]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<ActionResult<JobApplicationWithJoRes>> GetStudent([FromRoute] int jobApplicationId)
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
        public async Task<ActionResult<JobApplicationMinWithJoRes>> Post([FromBody] JobApplicationWithJoReq request)
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
