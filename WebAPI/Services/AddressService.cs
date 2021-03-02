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
        Address Create(AddressReq model);
    }

    /// <summary>
    /// Service for Address operations
    /// </summary>
    public class AddressService: IAddressService
    {
        private readonly StudentbyContext _context;

        public AddressService(StudentbyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create Address
        /// </summary>
        /// <param name="model">Address DTO</param>
        /// <returns>Address entity</returns>
        public Address Create(AddressReq model)
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
