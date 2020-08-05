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
    [Authorize(Roles = Role.Employee)]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IJobOfferService _jobOfferService;

        public EmployeeController(IJobOfferService jobOfferService)
        {
            _jobOfferService = jobOfferService;
        }

        // GET: api/student/job-offers
        [HttpGet("job-offers")]
        public async Task<ActionResult<IEnumerable<JobCreateResponse>>> GetJobOffers()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

                var response = await _jobOfferService.GetEmployeeJobOffersAsync(userId);

            if (response == null)
            {
                return StatusCode(404, "Nenalezeno"); 
            }            
            return StatusCode(200, response);
      
        }

        // POST: api/employee/job-offers
        [HttpPost("job-offers")]        
        public async Task<ActionResult<JobCreateResponse>> CreateJobOffer([FromBody] JobCreateRequest request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobOfferService.CreateJobOfferAsync(request, userId);
            return StatusCode(201, response);
        }

    }
}
