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
    }
}
