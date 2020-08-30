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
        Task<CustomerRegisterResponse> CreateEmployeeAsync(CustomerRegisterRequest model);
    }

    public class CustomerService: ICustomerService
    {
        private readonly StudentbyContext _context;
        private readonly IUserService _userService;

        public CustomerService(StudentbyContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<CustomerRegisterResponse> CreateEmployeeAsync(CustomerRegisterRequest model)
        {
            User user = await _userService.CreateUserAsync(model.Email, model.Password, Role.Customer);
            Group group = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId == model.GroupId);

            Customer customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                User = user,
                Group = group
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerRegisterResponse(customer);
        }
    }
}
