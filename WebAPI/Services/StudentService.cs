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
        Task<StudentMinRes> CreateAsync(StudentReq model);
        Task<bool> EditOperatorAsync(int studentId, StudentWithRoleReq model);
        Task<IEnumerable<StudentMinRes>> GetListAsync();
        Task<StudentWithAdActivRes> GetDetailAsync(int studentId);
        Task<int> GetStudentIdAsync(int userId);
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
        public async Task<IEnumerable<StudentMinRes>> GetListAsync()
        {
            var students = await _context.Students.ToListAsync();
            List<StudentMinRes> output = new List<StudentMinRes>();
            foreach (var student in students)
            {
                output.Add(new StudentMinRes(student));
            }
            return output;
        }

        /// <summary>
        /// Get detail of Student
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <returns>Student DTO</returns>
        public async Task<StudentWithAdActivRes> GetDetailAsync(int studentId)
        {
            var student = await _context.Students
                .Include(st => st.User)
                .Include(st => st.Address)
                .FirstOrDefaultAsync(st => st.StudentId == studentId);
            if (student == null)
            {
                return null;
            }
            return new StudentWithAdActivRes(student);
        }

        /// <summary>
        /// Create Student
        /// </summary>
        /// <param name="model">Student DTO</param>
        /// <returns>Student DTO</returns>
        public async Task<StudentMinRes> CreateAsync(StudentReq model)
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
            return new StudentMinRes(student);
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
                throw new AppLogicException("Neplatný požadavek");
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
                throw new AppLogicException("Neplatná změna role");
            }

            await _context.SaveChangesAsync();
            return true;

        }

        /// <summary>
        /// Get Student ID based on User ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>-1 when not found, otherwise Student ID</returns>
        public async Task<int> GetStudentIdAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(us => us.UserId == userId);
            if (user == null || user.StudentId == null)
            {
                return -1;
            }
            return user.StudentId.Value;
        }
    }
}
