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
    /// Request: Address 
    /// </summary>
    public class AddressReq
    {
        [DisplayName("Země")]
        [CustomRequired]
        public string Country { get; set; }
        [DisplayName("Město")]
        [CustomRequired]
        public string City { get; set; }
        [DisplayName("Ulice")]
        [CustomRequired]
        public string Street { get; set; }
        [DisplayName("Číslo")]
        [CustomRequired]
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
