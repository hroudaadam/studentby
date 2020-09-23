using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class AddressRequest
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

    public class AddressResponse
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

        public AddressResponse(Address address)
        {
            Country = address.Country;
            City = address.City;
            Street = address.Street;
            Number = address.Number;
        }
    }
}
