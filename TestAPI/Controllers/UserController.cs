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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/user/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserAuthenticateResponse>> Login(UserAuthenticateRequest request)
        {
            try
            {
                var response = await _userService.AuthenticateAsync(request);
                return Ok(response);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        // GET: api/User/
        [HttpGet("data")]
        public async Task<ActionResult<User>> GetUser()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);

            try
            {
                var user = await _userService.GetUserAsync(userId);
                return Ok(user);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
