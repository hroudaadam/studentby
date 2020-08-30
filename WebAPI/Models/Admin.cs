using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
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

    public class AdminRegisterRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
