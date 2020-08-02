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
    [Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IJobOfferService _jobOfferService;

        public EmployeeController(IJobOfferService jobOfferService)
        {
            _jobOfferService = jobOfferService;
        }

        // POST: api/employee/job-offers
        [HttpPost("job-offers")]
        [Authorize(Roles = Role.Employee)]
        public async Task<ActionResult<JobCreateResponse>> CreateJobOffer([FromBody] JobCreateRequest request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            try
            {
                var response = await _jobOfferService.CreateJobOfferAsync(request, userId);
                return StatusCode(201, response);
            }
            catch (AppException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

    }
}
