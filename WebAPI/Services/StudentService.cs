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
        Task<StudentRes> CreateAsync(StudentReq model);
        Task<bool> EditOperatorAsync(int studentId, StudentWithRoleReq model);
        Task<IEnumerable<StudentNameRes>> GetListAsync();
        Task<StudentWithActivRes> GetDetailAsync(int studentId);
    }

    /// <summary>
    /// Service for Student operations
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly StudentbyContext _context;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IJobApplicationService _jobApplicationService;

        public StudentService(StudentbyContext context, IUserService userService, IAddressService addressService, IJobApplicationService jobApplicationService)
        {
            _context = context;
            _userService = userService;
            _addressService = addressService;
            _jobApplicationService = jobApplicationService;
        }

        /// <summary>
        /// Get list of Students
        /// </summary>
        /// <returns>List of Student DTOs</returns>
        public async Task<IEnumerable<StudentNameRes>> GetListAsync()
        {
            var students = await _context.Students.ToListAsync();
            List<StudentNameRes> output = new List<StudentNameRes>();
            foreach (var student in students)
            {
                output.Add(new StudentNameRes(student));
            }
            return output;
        }

        /// <summary>
        /// Get detail of Student
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <returns>Student DTO</returns>
        public async Task<StudentWithActivRes> GetDetailAsync(int studentId)
        {
            var student = await _context.Students
                .Include(st => st.User)
                .Include(st => st.Address)
                .FirstOrDefaultAsync(st => st.StudentId == studentId);
            if (student == null)
            {
                return null;
            }
            return new StudentWithActivRes(student);
        }

        /// <summary>
        /// Create Student
        /// </summary>
        /// <param name="model">Student DTO</param>
        /// <returns>Student DTO</returns>
        public async Task<StudentRes> CreateAsync(StudentReq model)
        {
            // create user
            User user = await _userService.CreateAsync(model.Email, model.Password, UserRoles.StudentInact);
            // create address
            Address address = _addressService.Create(model.Address);

            // create student
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
            return new StudentRes(student);
        }

        /// <summary>
        /// Edit Student (as Operator)
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <param name="model">Student DTO</param>
        /// <returns>Bool if Student was found</returns>
        public async Task<bool> EditOperatorAsync(int studentId, StudentWithRoleReq model)
        {
            // route id and model id differs
            // !!! refactor -> method
            if (studentId != model.StudentId)
            {
                throw new StudentbyException("Neplatný požadavek");
            }

            var student = await _context.Students
                .Include(st => st.User)
                .FirstOrDefaultAsync(st => st.StudentId == studentId);
            // student not found
            if (student == null)
            {
                return false;
            }

            // student activation
            if (student.User.Role == UserRoles.StudentInact &&
                model.Role == UserRoles.Student)
            {
                // !!! if banned
                student.User.Role = UserRoles.Student;
            }
            // student deactivation
            else if (student.User.Role == UserRoles.Student &&
                model.Role == UserRoles.StudentInact)
            {
                await _jobApplicationService.CancelAllActiveAsync(studentId);
                student.User.Role = UserRoles.StudentInact;
            }
            // invalid role transation
            else
            {
                throw new StudentbyException("Neplatná změna role");
            }

            await _context.SaveChangesAsync();
            return true;

        }
    }
}
