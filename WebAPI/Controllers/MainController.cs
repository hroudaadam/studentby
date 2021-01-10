
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
        private readonly ITestService _testService;

        public MainController(IUserService userService, ITestService testService)
        {
            _userService = userService;
            _testService = testService;
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
            return StatusCode(204);
        }

        // GET: api/test
        [HttpGet("test")]
        public ActionResult Test()
        {          
            int i = new Random().Next(2);
            if (i == 0)
            {
                throw new AppLogicException("Logic chyba");
                
            }
            return StatusCode(200, new { name="Adam", surname="Hrouda" });
        }

        // GET: api/seed-db
        [HttpGet("seed-db")]
        public async Task<ActionResult> SeedDBAsync()
        {
            await _testService.SeedDbAsync();
            return Ok("Test");
        }

        // GET: api/clear-db
        [HttpGet("clear-db")]
        public async Task<ActionResult> ClearDBAsync()
        {
            await _testService.ClearDbAsync();
            return Ok("Test");
        }
    }
}
