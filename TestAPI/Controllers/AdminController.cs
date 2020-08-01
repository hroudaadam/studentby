using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/admin/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AdminRegisterResponse>> Register([FromBody] AdminRegisterRequest request)
        {
            try
            {
                var response = await _userService.CreateAdminAsync(request);
                return Ok(response);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/admin/test
        [AllowAnonymous]
        [HttpGet("test")]
        public async Task<ActionResult<User>> Test()
        {
            var request = HttpContext.Request;
            User user = new User
            {
                Email = "Helo"
            };
            return Ok(user);
        }
    }
}
