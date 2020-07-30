using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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
    [Authorize]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        // POST: api/job/create
        [HttpPost("create")]
        [Authorize(Roles = Role.Employee)]
        public async Task<ActionResult<JobCreateResponse>> Create(JobCreateRequest request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            try
            {
                var response = await _jobService.CreateJobOfferAsync(request, userId);
                return Ok(response);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/job/apply
        [HttpPost("apply")]
        [Authorize(Roles = Role.Student)]
        public async Task<ActionResult<JobApplicationCreateResponse>> Apply(JobApplicationCreateRequest request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            try
            {
                var response = await _jobService.ApplicationCreateAsync(request, userId);
                return Ok(response);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
