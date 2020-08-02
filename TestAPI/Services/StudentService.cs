using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Models;

namespace TestAPI.Services
{
    public interface IStudentService
    {
        Task<StudentRegisterResponse> CreateStudentAsync(StudentRegisterRequest model);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentbyTestContext _context;
        private readonly IUserService _userService;

        public StudentService(StudentbyTestContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<StudentRegisterResponse> CreateStudentAsync(StudentRegisterRequest model)
        {
            User user = await _userService.CreateUserAsync(model.Email, model.Password, Role.Student);

            Student student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth.Value,
                User = user
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new StudentRegisterResponse(student);
        }
    }
}
