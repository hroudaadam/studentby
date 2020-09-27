using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Helpers;

namespace WebAPI.Models
{
    /// <summary>
    /// Request: Student
    /// </summary>
    public class StudentReq
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public AddressRequest Address { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StudentRoleRequest
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public string Role { get; set; }
    }

    /// <summary>
    /// Response: Student
    /// </summary>
    public class StudentRes
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public StudentRes(Student student)
        {
            StudentId = student.StudentId;
            FirstName = student.FirstName;
            LastName = student.LastName;
            DateOfBirth = student.DateOfBirth;
        }
    }

    /// <summary>
    /// Response: Student with activated
    /// </summary>
    public class StudentWithActivRes
    {
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressRes Address { get; set; }
        public bool Activated { get; set; }

        public StudentWithActivRes(Student student)
        {
            StudentId = student.StudentId;
            Email = student.User.Email;
            FirstName = student.FirstName;
            LastName = student.LastName;
            DateOfBirth = student.DateOfBirth;
            Address = new AddressRes(student.Address);
            Activated = student.User.Role == UserRoles.Student;
        }
    }

    /// <summary>
    /// Response: Student detail
    /// </summary>
    public class StudentDetailRes
    {
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressRes Address { get; set; }

        public StudentDetailRes(Student student)
        {
            StudentId = student.StudentId;
            Email = student.User.Email;
            FirstName = student.FirstName;
            LastName = student.LastName;
            DateOfBirth = student.DateOfBirth;
            Address = new AddressRes(student.Address);
        }
    }

    /// <summary>
    /// Response: Student name
    /// </summary>
    public class StudentNameRes
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public StudentNameRes(Student student)
        {
            StudentId = student.StudentId;
            FirstName = student.FirstName;
            LastName = student.LastName;
        }
    }
}
