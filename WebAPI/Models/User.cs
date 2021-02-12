using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Helpers;

namespace WebAPI.Models
{
    /// <summary>
    /// Request: User
    /// </summary>
    public class UserReq
    {
        [DisplayName("Email")]
        [CustomRequired]
        [EmailAddress(ErrorMessage = "Emailová adresa není validní")]
        public string Email { get; set; }

        [DisplayName("Heslo")]
        [CustomRequired]
        [CustomMinLength(8)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Response: User
    /// </summary>
    public class UserRes
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public UserRes(User user, string token)
        {
            Email = user.Email;
            Token = token;
            Role = user.Role;
        }
    }

    /// <summary>
    /// Request: Password
    /// </summary>
    public class PasswordReq
    {
        [CustomRequired]
        public string Secret { get; set; }

        [DisplayName("Heslo")]
        [CustomRequired]
        [CustomMinLength(8)]
        public string Password { get; set; }
    }

}
