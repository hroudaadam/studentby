using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;

namespace TestAPI.Models
{
    public class AdminRegisterResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }

        public AdminRegisterResponse(User user)
        {
            UserId = user.UserId;
            Email = user.Email;
        }
    }
}
