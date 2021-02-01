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
    /// <summary>
    /// Controller for Customer
    /// </summary>
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST: api/customers
        [HttpPost]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<ActionResult<CustomerRes>> Post([FromBody] CustomerReq request)
        {
            var response = await _customerService.CreateAsync(request);
            return StatusCode(201, response);
        }

        // POST: api/customers/profile
        [HttpGet("profile")]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<CustomerWithGrRes>> GetProfile()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            var response = await _customerService.GetDetailAsync(userId);
            if (response == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, response);
        }
    }
}
