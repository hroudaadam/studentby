﻿using System;
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
    /// Request: Customer
    /// </summary>
    public class CustomerReq
    {
        [DisplayName("Email")]
        [CustomRequired]
        [EmailAddress(ErrorMessage ="Emailová adresa není validní")]
        public string Email { get; set; }

        [DisplayName("ID skupiny")]
        [CustomRequired]
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
    /// Response: Customer with secret
    /// </summary>
    public class CustomerWithSecRes
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Secret { get; set; }

        public CustomerWithSecRes(Customer customer, string sec)
        {
            CustomerId = customer.CustomerId;
            Email = customer.User.Email;
            Secret = sec;
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
