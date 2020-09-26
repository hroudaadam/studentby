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
        [Authorize(Roles = UserRoles.Student + "," + UserRoles.Operator)]
        public async Task<ActionResult<IEnumerable<JobApplicationSimpleRes>>> GetAll()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            string userRole = await _userService.GetUserRole(userId);
            IEnumerable<JobApplicationSimpleRes> response;

            if (userRole == UserRoles.Student)
            {
                response = await _jobApplicationService.GetListStudentAsync(userId);
            }
            else if (userRole == UserRoles.Operator)
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
        [Authorize(Roles = UserRoles.Student + "," + UserRoles.Operator)]
        public async Task<ActionResult<IJobApplicationDetail>> Get([FromRoute] int jobApplicationId)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            string userRole = await _userService.GetUserRole(userId);
            IJobApplicationDetail response;

            if (userRole == UserRoles.Student)
            {
                response = await _jobApplicationService.GetDetailStudentAsync(jobApplicationId, userId);
            }
            else if (userRole == UserRoles.Operator)
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

        // GET: api/job-applications/1/result
        [HttpGet("{jobApplicationId}/result")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<JobApplicationWithStudRes>> GetResult([FromRoute] int jobApplicationId)
        {
            var response = await _jobApplicationService.GetResultAsync(jobApplicationId);

            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // POST: api/job-applications
        [HttpPost]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<ActionResult<JobApplicationRes>> Post([FromBody] JobApplicationReq request)
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
            [FromBody] JobApplicationDetailReq request)
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
