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
        [Authorize(Roles = Role.Student + "," + Role.Customer)]
        public async Task<ActionResult<IEnumerable<JobOfferSimpleResponse>>> GetAll()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            string userRole = await _userService.GetUserRole(userId);
            IEnumerable<JobOfferSimpleResponse> response;

            if (userRole == Role.Student)
            {
                response = await _jobOfferService.GetListStudentAsync(userId);
            }
            else if (userRole == Role.Customer)
            {
                response = await _jobOfferService.GetListCustomerAsync(userId);
            }
            else
            {
                throw new Exception();
            }

            return StatusCode(200, response);
        }

        // GET: api/job-offers/1
        [HttpGet("{jobOfferId}")]
        [Authorize(Roles = Role.Student + "," + Role.Customer)]
        public async Task<ActionResult<IJobOfferDetail>> Get([FromRoute] int jobOfferId)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            string userRole = await _userService.GetUserRole(userId);
            IJobOfferDetail response;

            if (userRole == Role.Student)
            {
                response = await _jobOfferService.GetDetailStudentAsync(jobOfferId);
            }
            else if (userRole == Role.Customer)
            {
                response = await _jobOfferService.GetDetailCustomerAsync(jobOfferId, userId);
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
        [Authorize(Roles = Role.Customer)]
        public async Task<ActionResult<JobOfferResponse>> CreateJobOffer([FromBody] JobOfferRequest request)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            var response = await _jobOfferService.CreateAsync(request, userId);
            return StatusCode(201, response);
        }
    }
}