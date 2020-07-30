using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;

namespace TestAPI.Models
{
    public class EmployeeRegisterResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string CompanyTitle { get; set; }


        public EmployeeRegisterResponse(Employee employee)
        {
            UserId = employee.User.UserId;
            Email = employee.User.Email;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            CompanyTitle = employee.Company.Title;
        }
    }
}
