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
    [Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserService _userService;

        public EmployeeController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/employee/register
        [Authorize(Roles = Role.Admin)]
        [HttpPost("register")]
        public async Task<ActionResult<EmployeeRegisterResponse>> Register([FromBody] EmployeeRegisterRequest request)
        {
            try
            {
                var response = await _userService.CreateEmployeeAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
