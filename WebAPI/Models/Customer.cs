using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int GroupId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomerResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CustomerResponse(Customer customer)
        {
            UserId = customer.User.UserId;
            Email = customer.User.Email;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
        }
    }
}
