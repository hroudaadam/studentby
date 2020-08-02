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
        [HttpPost()]
        public async Task<ActionResult<StudentRegisterResponse>> CreateStudent([FromBody] StudentRegisterRequest request)
        {
            try
            {
                var response = await _studentService.CreateStudentAsync(request);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        // GET: api/student/job-offers
        [HttpGet("job-offers")]
        public async Task<ActionResult<IEnumerable<JobCreateResponse>>> GetJobOffers()
        {
            try
            {
                var response = await _jobOfferService.GetJobOffersAsync();
                return StatusCode(200, response);
            }
            catch (AppException ex)
            {
                // 404 response???
                return StatusCode(400, ex.Message);
            }
        }

        // GET: api/student/job-offers/:id
        [HttpGet("job-offers/{id}")]
        public async Task<ActionResult<JobOffer>> GetJobOfferDetail([FromRoute] int id)
        {
            try
            {
                var response = await _jobOfferService.GetJobOfferDetailAsync(id);
                return StatusCode(200, response);
            }
            catch (AppException ex)
            {
                // 404 ??
                return StatusCode(400, ex.Message);
            }
        }

        // POST: api/student/job-applications
        [HttpPost("job-applications")]
        public async Task<ActionResult<JobApplicationCreateResponse>> CreateJobApplication([FromBody] JobApplicationCreateRequest request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            try
            {
                var response = await _jobApplicationService.CreateJobApplicationAsync(request, userId);
                return StatusCode(201, response);
            }
            catch (AppException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        // GET: api/student/job-applications
        [HttpGet("job-applications")]
        [Authorize(Roles = Role.Student)]
        public async Task<ActionResult<IEnumerable<JobCreateResponse>>> GetJobApplications()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            try
            {
                var response = await _jobApplicationService.GetStudentApplicationsAsync(userId);
                return StatusCode(200, response);
            }
            catch (AppException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
