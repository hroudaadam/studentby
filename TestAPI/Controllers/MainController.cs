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
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
{
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
        public async Task<ActionResult<UserAuthenticateResponse>> Login(UserAuthenticateRequest request)
        {
            var response = await _userService.AuthenticateAsync(request);
            return StatusCode(200, response);

        }

        // POST: api/admin
        [HttpPost("admin")]
        public async Task<ActionResult<AdminRegisterResponse>> CreateAdmin([FromBody] AdminRegisterRequest request)
        {
            var response = await _userService.CreateAdminAsync(request);
            return StatusCode(201, response);
        }

        // GET: api/test
        [HttpGet("test")]
        public ActionResult<int> Test()
        {
            throw new StudentbyException("Bussines logic chyba");
        }
    }
}
