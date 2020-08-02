using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;

namespace TestAPI.Models
{
    public class UserAuthenticateResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public UserAuthenticateResponse(User user, string token)
        {
            UserId = user.UserId;
            Email = user.Email;
            Token = token;
            Role = user.Role;
        }
    }
}
