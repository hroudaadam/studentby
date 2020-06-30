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
    public interface IStudentService
    {
        Task<StudentRegisterResponse> CreateAsync(StudentRegisterRequest model, User user);
    }

    public class StudentService: IStudentService
    {
        private readonly StudentbyTestContext _context;

        public StudentService(StudentbyTestContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<StudentRegisterResponse> CreateAsync(StudentRegisterRequest model, User user)
        {
            user.Role = Role.Student;

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
