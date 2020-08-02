using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Models;

namespace TestAPI.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeRegisterResponse> CreateEmployeeAsync(EmployeeRegisterRequest model);
    }

    public class EmployeeService: IEmployeeService
    {
        private readonly StudentbyTestContext _context;
        private readonly IUserService _userService;

        public EmployeeService(StudentbyTestContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<EmployeeRegisterResponse> CreateEmployeeAsync(EmployeeRegisterRequest model)
        {
            User user = await _userService.CreateUserAsync(model.Email, model.Password, Role.Employee);
            Company company = await _context.Companies.SingleOrDefaultAsync(x => x.CompanyId == model.CompanyId);

            Employee employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                User = user,
                Company = company
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new EmployeeRegisterResponse(employee);
        }
    }
}
