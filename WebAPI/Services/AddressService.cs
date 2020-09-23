using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IAddressService
    {
        Address Create(AddressRequest model);
    }

    public class AddressService: IAddressService
    {
        private readonly StudentbyContext _context;

        public AddressService(StudentbyContext context)
        {
            _context = context;
        }

        public Address Create(AddressRequest model)
        {
            var address = new Address
            {
                Country = model.Country,
                City = model.City,
                Street = model.Street,
                Number = model.Number
            };
            return address;
        }
    }
}
