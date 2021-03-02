
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Main controller
    /// </summary>
    [ApiController]
    [Route("api")]
    public class MainController : ControllerBase
    {
        private readonly IUserService _userService;

        public MainController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/login
        [HttpPost("login")]
        public async Task<ActionResult<UserRes>> Login(UserReq request)
        {
            var response = await _userService.AuthenticateAsync(request);
            return StatusCode(200, response);
        }

        // GET: api/admin
        [HttpGet("admin")]
        public async Task<ActionResult> Admin()
        {
            if (await _userService.EnsureOperatorAsync())
            {
                return StatusCode(201);
            }
            return StatusCode(400);
        }

        // PUT: api/user/password
        [HttpPut("user/password")]
        public async Task<ActionResult> SetPassword([FromBody] PasswordReq request)
        {
            await _userService.SetPassword(request);
            return StatusCode(204);
        }
    }
}
