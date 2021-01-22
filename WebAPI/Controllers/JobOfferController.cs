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

        public JobOfferController(IJobOfferService jobOfferService)
        {
            _jobOfferService = jobOfferService;
        }

        // GET: api/job-offers
        [HttpGet]
        [Authorize(Roles = UserRoles.Student + "," + UserRoles.Operator)]
        public async Task<ActionResult<IEnumerable<JobOfferMinRes>>> GetList()
        {
            var response = await _jobOfferService.GetListAsync();
            return StatusCode(200, response);
        }

        // GET: api/job-offers/customer-view
        [HttpGet("customer-view")]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<IEnumerable<JobOfferMinRes>>> GetListCustomer()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            var response = await _jobOfferService.GetListCustomerAsync(userId);
            return StatusCode(200, response);
        }

        // GET: api/job-offers/1/student-view
        [HttpGet("{jobOfferId}/student-view")]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<ActionResult<JobOfferRes>> GetStudent([FromRoute] int jobOfferId)
        {
            var response = await _jobOfferService.GetDetailStudentAsync(jobOfferId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // GET: api/job-offers/1/customer-view
        [HttpGet("{jobOfferId}/customer-view")]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<JobOfferRes>> GetCustomer([FromRoute] int jobOfferId)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            var response = await _jobOfferService.GetDetailCustomerAsync(jobOfferId, userId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }

        // GET: api/job-offers/1/operator-view
        [HttpGet("{jobOfferId}/operator-view")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<JobOfferWithJasRes>> GetOperator([FromRoute] int jobOfferId)
        {
            var response = await _jobOfferService.GetDetailOperatorAsync(jobOfferId);
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