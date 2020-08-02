using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Role.Operator)]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public OperatorController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // POST: api/operator/employee
        [HttpPost("employee")]
        public async Task<ActionResult<EmployeeRegisterResponse>> CreateEmployee([FromBody] EmployeeRegisterRequest request)
        {
            try
            {
                var response = await _employeeService.CreateEmployeeAsync(request);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

    }
}
