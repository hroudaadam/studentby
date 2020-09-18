using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentRequest
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
    }

    /// <summary>
    /// 
    /// </summary>
    public class StudentResponse
    {
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public StudentResponse(Student student)
        {
            StudentId = student.StudentId;
            Email = student.User.Email;
            FirstName = student.FirstName;
            LastName = student.LastName;
            DateOfBirth = student.DateOfBirth;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StudentSimpleResponse
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public StudentSimpleResponse(Student student)
        {
            StudentId = student.StudentId;
            FirstName = student.FirstName;
            LastName = student.LastName;
        }
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
}
