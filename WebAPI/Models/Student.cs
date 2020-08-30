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
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public StudentResponse(Student student)
        {
            UserId = student.User.UserId;
            Email = student.User.Email;
            FirstName = student.FirstName;
            LastName = student.LastName;
            DateOfBirth = student.DateOfBirth;
        }
    }
}
