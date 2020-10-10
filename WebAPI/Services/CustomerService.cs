using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Helpers;
using WebAPI.Entities;

namespace WebAPI.Services
{
    public interface ICustomerService
    {
        Task<CustomerRes> CreateAsync(CustomerReq model);
    }

    /// <summary>
    /// Service for Customer operations
    /// </summary>
    public class CustomerService: ICustomerService
    {
        private readonly StudentbyContext _context;
        private readonly IUserService _userService;

        public CustomerService(StudentbyContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        /// <summary>
        /// Create new Customer
        /// </summary>
        /// <param name="model">Customer DTO</param>
        /// <returns>Customer DTO</returns>
        public async Task<CustomerRes> CreateAsync(CustomerReq model)
        {
            // !!! autogen password
            string password = "test";
            // create user
            User user = await _userService.CreateAsync(model.Email, password, UserRoles.Customer);
            Group group = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId == model.GroupId);

            // group not found
            if (group == null)
            {
                throw new StudentbyException("Skupina neexistuje");
            }

            // create customer
            Customer customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                User = user,
                Group = group
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return new CustomerRes(customer);
        }
    }
}
