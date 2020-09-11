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
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IJobOfferService _jobOfferService;
        private readonly ICustomerService _customerService;

        public CustomerController(IJobOfferService jobOfferService, ICustomerService customerService)
        {
            _jobOfferService = jobOfferService;
            _customerService = customerService;
        }

        // POST: api/customers
        [HttpPost]
        [Authorize(Roles = Role.Operator)]
        public async Task<ActionResult<CustomerResponse>> Post([FromBody] CustomerRequest request)
        {
            var response = await _customerService.CreateAsync(request);
            return StatusCode(201, response);
        }

        // GET: api/customer/job-offers
        /*[HttpGet("job-offers")]
        public async Task<ActionResult<IEnumerable<JobOfferSimpleResponse>>> GetJobOffers()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobOfferService.GetListCustomerAsync(userId);         
            return StatusCode(200, response);      
        }*/

        // GET: api/customer/job-offers/:id
        /*        [HttpGet("job-offers/{id}")]
                public async Task<ActionResult<JobOfferDetailWithStudentsResponse>> GetJobOfferDetail([FromRoute] int id)
                {
                    int userId = int.Parse(HttpContext.User.Identity.Name);

                    var response = await _jobOfferService.GetDetailCustomerAsync(id, userId);  
                    if (response == null)
                    {
                        return StatusCode(404);
                    }
                    return StatusCode(200, response);
                }*/

        // POST: api/customer/job-offers
        /*        [HttpPost("job-offers")]        
                public async Task<ActionResult<JobOfferResponse>> CreateJobOffer([FromBody] JobOfferRequest request)
                {
                    int userId = int.Parse(HttpContext.User.Identity.Name);

                    var response = await _jobOfferService.CreateAsync(request, userId);
                    return StatusCode(201, response);
                }*/

    }
}
