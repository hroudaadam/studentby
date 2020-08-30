using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthenticateResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            UserId = user.UserId;
            Email = user.Email;
            Token = token;
            Role = user.Role;
        }
    }
}
