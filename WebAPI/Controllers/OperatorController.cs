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
        private readonly ICustomerService Customer;

        public OperatorController(ICustomerService customerService)
        {
            Customer = customerService;
        }

        // POST: api/operator/employee
        [HttpPost("employee")]
        [AllowAnonymous] // zatím
        public async Task<ActionResult<CustomerRegisterResponse>> CreateEmployee([FromBody] CustomerRegisterRequest request)
        {
            var response = await Customer.CreateEmployeeAsync(request);
            return StatusCode(201, response);
        }

    }
}
