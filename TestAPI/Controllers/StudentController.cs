using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
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
        public async Task<ActionResult<StudentRegisterResponse>> CreateStudent([FromBody] StudentRegisterRequest request)
        {
            var response = await _studentService.CreateStudentAsync(request);
            return StatusCode(201, response);

        }

        // GET: api/student/job-offers
        [HttpGet("job-offers")]
        public async Task<ActionResult<IEnumerable<JobCreateResponse>>> GetJobOffers()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobOfferService.GetStudentJobOffersAsync(userId);
            return StatusCode(200, response);
        }

        // GET: api/student/job-offers/:id
        [HttpGet("job-offers/{id}")]
        public async Task<ActionResult<JobOffer>> GetJobOfferDetail([FromRoute] int id)
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
        public async Task<ActionResult<JobApplicationCreateResponse>> CreateJobApplication([FromBody] JobApplicationCreateRequest request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobApplicationService.CreateJobApplicationAsync(request, userId);
            return StatusCode(201, response);
        }

        // GET: api/student/job-applications
        [HttpGet("job-applications")]
        public async Task<ActionResult<IEnumerable<JobCreateResponse>>> GetJobApplications()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobApplicationService.GetStudentApplicationsAsync(userId);
            return StatusCode(200, response);
        }

        // DELETE: api/student/job-applications/:id
        [HttpDelete("job-applications/{id}")]
        public async Task<ActionResult<User>> CancelApplication([FromRoute] int id)
        {
            bool found = await _jobApplicationService.CancelApplicationsAsync(id);
            if (found)
            {
                return StatusCode(204);
            }
            return StatusCode(404);
        }
    }
}
