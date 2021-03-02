using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Helpers;
using WebAPI.Entities;
using System.Text;

namespace WebAPI.Services
{
    public interface ICustomerService
    {
        Task<CustomerWithSecRes> CreateAsync(CustomerReq model);
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
        public async Task<CustomerWithSecRes> CreateAsync(CustomerReq model)
        {
            Group group = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId == model.GroupId);

            // group not found
            if (group == null)
            {
                throw new AppLogicException("Skupina neexistuje");
            }

            // autogen password
            string password = GeneratePassword();
            
            // create user
            User user = await _userService.CreateAsync(model.Email, password, UserRoles.Customer);
            //string hash = Encoding.GetEncoding(1252).GetString(user.PasswordHash);
            string secret = BitConverter.ToString(user.PasswordHash).Replace("-", "");
            secret = secret + ":" + user.Email;

            // create customer
            Customer customer = new Customer
            {
                User = user,
                Group = group
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return new CustomerWithSecRes(customer, secret);
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

        /// <summary>
        /// Generate random password
        /// </summary>
        /// <returns></returns>
        private string GeneratePassword()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789?!.";
            string password = ";";
            var random = new Random();

            for (int i = 0; i < 8; i++)
            {
                password += chars[random.Next(chars.Length)];
            }

            return password;
        }
    }
}
