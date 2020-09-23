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
        Task<IEnumerable<StudentSimpleResponse>> GetListAsync();
        Task<StudentResponse> GetAsync(int studentId);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentbyContext _context;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;

        public StudentService(StudentbyContext context, IUserService userService, IAddressService addressService)
        {
            _context = context;
            _userService = userService;
            _addressService = addressService;
        }

        public async Task<IEnumerable<StudentSimpleResponse>> GetListAsync()
        {
            var students = await _context.Students.ToListAsync();
            List<StudentSimpleResponse> output = new List<StudentSimpleResponse>();
            foreach (var student in students)
            {
                output.Add(new StudentSimpleResponse(student));
            }
            return output;
        }

        public async Task<StudentResponse> GetAsync(int studentId)
        {
            var student = await _context.Students
                .Include(st => st.User)
                .FirstOrDefaultAsync(st => st.StudentId == studentId);
            if (student == null)
            {
                return null;
            }
            return new StudentResponse(student);
        }

        public async Task<StudentResponse> CreateAsync(StudentRequest model)
        {
            User user = await _userService.CreateUserAsync(model.Email, model.Password, UserRoles.StudentUnver);
            Address address = _addressService.Create(model.Address);

            Student student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth.Value,
                User = user,
                Address = address
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
            if (student.User.Role == UserRoles.StudentUnver &&
                model.Role == UserRoles.Student)
            {
                // má ban??
                student.User.Role = UserRoles.Student;
            }
            // deaktivace studentského účtu
            else if (student.User.Role == UserRoles.Student &&
                model.Role == UserRoles.StudentUnver)
            {
                // má nějaké aktivity??
                student.User.Role = UserRoles.StudentUnver;
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
