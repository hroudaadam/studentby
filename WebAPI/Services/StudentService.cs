using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IStudentService
    {
        Task<StudentResponse> CreateAsync(StudentRequest model);
        Task<bool> ChangeRoleAsync(int studentId, StudentRoleRequest model);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentbyContext _context;
        private readonly IUserService _userService;

        public StudentService(StudentbyContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<StudentResponse> CreateAsync(StudentRequest model)
        {
            User user = await _userService.CreateUserAsync(model.Email, model.Password, Role.StudentUnver);

            Student student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth.Value,
                User = user
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new StudentResponse(student);
        }

        public async Task<bool> ChangeRoleAsync(int studentId, StudentRoleRequest model)
        {
            if (studentId != model.StudentId)
            {
                throw new StudentbyException("Neplatný požadavek");
            }

            var student = await _context.Students
                .Include(st => st.User)
                .FirstOrDefaultAsync(st => st.StudentId == studentId);
            if (student == null)
            {
                return false;
            }

            // aktivace studentského účtu
            if (student.User.Role == Role.StudentUnver &&
                model.Role == Role.Student)
            {
                // má ban??
                student.User.Role = Role.Student;
            }
            // deaktivace studentského účtu
            else if (student.User.Role == Role.Student &&
                model.Role == Role.StudentUnver)
            {
                // má nějaké aktivity??
                student.User.Role = Role.StudentUnver;
            }
            else
            {
                throw new StudentbyException("Neplatný požadavek");
            }

            await _context.SaveChangesAsync();
            return true;

        }
    }
}
