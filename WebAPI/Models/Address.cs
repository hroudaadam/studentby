using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    /// <summary>
    /// Request: Address 
    /// </summary>
    public class AddressReq
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Number { get; set; }
    }

    /// <summary>
    /// Response: Address
    /// </summary>
    public class AddressRes
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

        public AddressRes(Address address)
        {
            Country = address.Country;
            City = address.City;
            Street = address.Street;
            Number = address.Number;
        }
    }
}
