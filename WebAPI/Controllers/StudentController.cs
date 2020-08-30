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
    [Route("api/[controller]")]
    [Authorize(Roles = Role.Student)]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IJobOfferService _jobOfferService;
        private readonly IJobApplicationService _jobApplicationService;

        public StudentController(
            IStudentService studentService,
            IJobOfferService jobOfferService,
            IJobApplicationService jobApplicationService)
        {
            _jobOfferService = jobOfferService;
            _studentService = studentService;
            _jobApplicationService = jobApplicationService;
        }

        // POST: api/student
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<StudentResponse>> CreateStudent([FromBody] StudentRequest request)
        {
            var response = await _studentService.CreateStudentAsync(request);
            return StatusCode(201, response);

        }

        // GET: api/student/job-offers
        [HttpGet("job-offers")]
        public async Task<ActionResult<IEnumerable<JobOfferSimpleResponse>>> GetJobOffers()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobOfferService.GetJobOffersStudentAsync(userId);
            return StatusCode(200, response);
        }

        // GET: api/student/job-offers/:id
        [HttpGet("job-offers/{id}")]
        public async Task<ActionResult<JobOfferDetailResponse>> GetJobOfferDetail([FromRoute] int id)
        {
            var response = await _jobOfferService.GetJobOfferDetailStudentAsync(id);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response); 
        }

        // POST: api/student/job-applications
        [HttpPost("job-applications")]
        public async Task<ActionResult<JobApplicationSimpleResponse>> CreateJobApplication([FromBody] JobApplicationRequest request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobApplicationService.CreateJobApplicationAsync(request, userId);
            return StatusCode(201, response);
        }

        // GET: api/student/job-applications
        [HttpGet("job-applications")]
        public async Task<ActionResult<IEnumerable<JobApplicationSimpleResponse>>> GetJobApplications()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobApplicationService.GetApplicationsStudentAsync(userId);
            return StatusCode(200, response);
        }

        // GET: api/student/job-applications/:id
        [HttpGet("job-applications/{id}")]
        public async Task<ActionResult<JobApplicationDetailResponse>> GetJobApplicationDetail([FromRoute] int id)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobApplicationService.GetApplicationDetailStudentAsync(id, userId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // DELETE: api/student/job-applications/:id
        [HttpDelete("job-applications/{id}")]
        public async Task<ActionResult> CancelApplication([FromRoute] int id)
        {
            bool found = await _jobApplicationService.CancelJobApplicationAsync(id);
            if (found)
            {
                return StatusCode(204);
            }
            return StatusCode(404);
        }
    }
}
