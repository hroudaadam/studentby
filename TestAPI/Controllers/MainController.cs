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
            try
            {
                var response = await _userService.AuthenticateAsync(request);
                return StatusCode(201, response);
            }
            catch (AppException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        // POST: api/admin
        [HttpPost("admin")]
        public async Task<ActionResult<AdminRegisterResponse>> CreateAdmin([FromBody] AdminRegisterRequest request)
        {
            try
            {
                var response = await _userService.CreateAdminAsync(request);
                return StatusCode(201, response);
            }
            catch (AppException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        // GET: api/test
        [HttpGet("test")]
        public ActionResult<int> Test()
        {
            Console.WriteLine("Ok");
            Debug.WriteLine("Ok");
            return StatusCode(200, 3135);
        }
    }
}
