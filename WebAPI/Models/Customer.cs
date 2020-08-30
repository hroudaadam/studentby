using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class CustomerRegisterRequest
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

    public class CustomerRegisterResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string GroupTitle { get; set; }


        public CustomerRegisterResponse(Customer customer)
        {
            UserId = customer.User.UserId;
            Email = customer.User.Email;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            GroupTitle = customer.Group.Title;
        }
    }
}
