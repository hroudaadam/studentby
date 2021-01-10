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
    /// Controller for JobOffer
    /// </summary>
    [Route("api/job-offers")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        private readonly IJobOfferService _jobOfferService;
        private readonly IUserService _userService;

        public JobOfferController(IJobOfferService jobOfferService, IUserService userService)
        {
            _jobOfferService = jobOfferService;
            _userService = userService;
        }

        // GET: api/job-offers
        [HttpGet()]
        [Authorize(Roles = UserRoles.Student + "," + UserRoles.Operator)]
        public async Task<ActionResult<IEnumerable<JobOfferSimpleRes>>> GetList()
        {
            var response = await _jobOfferService.GetListAsync();
            return StatusCode(200, response);
        }

        // GET: api/job-offers/customer
        [HttpGet("customer")]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<IEnumerable<JobOfferSimpleRes>>> GetListAsCustomer()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            var response = await _jobOfferService.GetListCustomerAsync(userId);
            return StatusCode(200, response);
        }

        // GET: api/job-offers/1/customer
        [HttpGet("{jobOfferId}/customer")]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<JobOfferWithGrAdRes>> GetAsCustomer([FromRoute] int jobOfferId)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            var response = await _jobOfferService.GetDetailCustomerAsync(jobOfferId, userId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // GET: api/job-offers/1/operator
        [HttpGet("{jobOfferId}/operator")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<JobOfferWithJasRes>> GetAsOperator([FromRoute] int jobOfferId)
        {
            var response = await _jobOfferService.GetDetailOperatorAsync(jobOfferId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // GET: api/job-offers/1
        [HttpGet("{jobOfferId}")]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<ActionResult<JobOfferWithGrAdRes>> Get([FromRoute] int jobOfferId)
        {
            var response = await _jobOfferService.GetDetailAsync(jobOfferId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }


        // POST: api/job-offers
        [HttpPost]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<JobOfferRes>> Post([FromBody] JobOfferReq request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobOfferService.CreateAsync(request, userId);
            return StatusCode(201, response);
        }

        // DELETE: api/job-offers/1
        [HttpDelete("{jobOfferId}")]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<IActionResult> Delete([FromRoute] int jobOfferId)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            bool found = await _jobOfferService.DeleteAsync(jobOfferId, userId);
            if (found)
            {
                return StatusCode(204);
            }
            return StatusCode(404);

        }
    }
}