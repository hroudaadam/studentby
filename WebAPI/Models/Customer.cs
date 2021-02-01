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
        public int GroupId { get; set; }
    }

    /// <summary>
    /// Response: Customer 
    /// </summary>
    public class CustomerRes
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }

        public CustomerRes(Customer customer)
        {
            CustomerId = customer.CustomerId;
            Email = customer.User.Email;
        }
    }

    /// <summary>
    /// Response: Customer with Group
    /// </summary>
    public class CustomerWithGrRes
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }

        public GroupRes Group { get; set; }

        public CustomerWithGrRes(Customer customer)
        {
            CustomerId = customer.CustomerId;
            Email = customer.User.Email;
            Group = new GroupRes(customer.Group);
        }
    }
}
