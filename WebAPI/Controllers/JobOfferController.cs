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
        [HttpGet]
        [Authorize(Roles = UserRoles.Student + "," + UserRoles.Customer + "," + UserRoles.Operator)]
        public async Task<ActionResult<IEnumerable<JobOfferSimpleRes>>> GetList()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            string userRole = await _userService.GetRole(userId);
            IEnumerable<JobOfferSimpleRes> response;

            if (userRole == UserRoles.Student)
            {
                response = await _jobOfferService.GetListStudentAsync(userId);
            }
            else if (userRole == UserRoles.Customer)
            {
                response = await _jobOfferService.GetListCustomerAsync(userId);
            }
            else if (userRole == UserRoles.Operator)
            {
                response = await _jobOfferService.GetListOperatorAsync();
            }
            else
            {
                throw new Exception("Nevalidní role");
            }
            return StatusCode(200, response);
        }

        // GET: api/job-offers/1
        [HttpGet("{jobOfferId}")]
        [Authorize(Roles = UserRoles.Student + "," + UserRoles.Customer + "," + UserRoles.Operator)]
        public async Task<ActionResult<IJobOfferDetail>> Get([FromRoute] int jobOfferId)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            string userRole = await _userService.GetRole(userId);
            IJobOfferDetail response;

            if (userRole == UserRoles.Student)
            {
                response = await _jobOfferService.GetDetailStudentAsync(jobOfferId);
            }
            else if (userRole == UserRoles.Customer)
            {
                response = await _jobOfferService.GetDetailCustomerAsync(jobOfferId, userId);
            }
            else if (userRole == UserRoles.Operator)
            {
                response = await _jobOfferService.GetDetailOperatorAsync(jobOfferId);
            }
            else
            {
                throw new Exception();
            }

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