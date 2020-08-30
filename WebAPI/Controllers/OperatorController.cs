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
    [Authorize(Roles = Role.Operator)]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public OperatorController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST: api/operator/
        [HttpPost()]
        public ActionResult<bool> CreateOperator()
        {
            return StatusCode(200, true);
        }

        // POST: api/operator/customer
        [HttpPost("customer")]
        public async Task<ActionResult<CustomerResponse>> CreateCustomer([FromBody] CustomerRequest request)
        {
            var response = await _customerService.CreateCustomerAsync(request);
            return StatusCode(201, response);
        }

    }
}
