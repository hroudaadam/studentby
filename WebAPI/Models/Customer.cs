using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    /// <summary>
    /// Request: Customer
    /// </summary>
    public class CustomerReq
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int GroupId { get; set; }
    }

    /// <summary>
    /// Response: Customer 
    /// </summary>
    public class CustomerRes
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CustomerRes(Customer customer)
        {
            CustomerId = customer.CustomerId;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
        }
    }

    /// <summary>
    /// Response: Customer detail
    /// </summary>
    public class CustomerDetailRes
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GroupRes Group { get; set; }

        public CustomerDetailRes(Customer customer)
        {
            CustomerId = customer.CustomerId;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Email = customer.User.Email;
        }
    }
}
