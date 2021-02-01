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
        Task<CustomerWithGrRes> GetDetailAsync(int userId);
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
                throw new AppLogicException("Skupina neexistuje");
            }

            // create customer
            Customer customer = new Customer
            {
                User = user,
                Group = group
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return new CustomerRes(customer);
        }

        /// <summary>
        /// Get detail of Customer
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Customer DTO</returns>
        public async Task<CustomerWithGrRes> GetDetailAsync(int userId)
        {
            var user = await _context.Users
                .Where(us => us.UserId == userId)
                .Include(us => us.Customer)
                    .ThenInclude(cu => cu.Group)
                .FirstOrDefaultAsync();
            if (user == null || user.Customer == null)
            {
                return null;
            }
            return new CustomerWithGrRes(user.Customer);
        }
    }
}
