using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Role.Customer)]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IJobOfferService _jobOfferService;

        public CustomerController(IJobOfferService jobOfferService)
        {
            _jobOfferService = jobOfferService;
        }

        // GET: api/customer/job-offers
        [HttpGet("job-offers")]
        public async Task<ActionResult<IEnumerable<JobOfferResponse>>> GetJobOffers()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobOfferService.GetCompanyJobOffersAsync(userId);         
            return StatusCode(200, response);      
        }

        // GET: api/customer/job-offers/:id
        [HttpGet("job-offers/{id}")]
        public async Task<ActionResult<JobOfferDetailCustomerResponse>> GetJobOfferDetail([FromRoute] int id)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobOfferService.GetJobOfferDetailEmployeeAsync(id, userId);  
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // POST: api/customer/job-offers
        [HttpPost("job-offers")]        
        public async Task<ActionResult<JobOfferCreateResponse>> CreateJobOffer([FromBody] JobOfferCreateRequest request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobOfferService.CreateJobOfferAsync(request, userId);
            return StatusCode(201, response);
        }

    }
}
