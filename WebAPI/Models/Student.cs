﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Email")]
        [CustomRequired]
        [EmailAddress(ErrorMessage = "Emailová adresa není validní")]
        public string Email { get; set; }

        [DisplayName("Heslo")]
        [CustomRequired]
        [CustomMinLength(8)]
        public string Password { get; set; }

        [DisplayName("Jména")]
        [CustomRequired]
        public string FirstName { get; set; }

        [DisplayName("Příjmení")]
        [CustomRequired]
        public string LastName { get; set; }

        [DisplayName("Datum narození")]
        [CustomRequired]
        [MinAge(18)]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Adresa")]
        [CustomRequired]
        public AddressReq Address { get; set; }
    }

    /// <summary>
    /// Request: Student with role
    /// </summary>
    public class StudentWithRoleReq
    {
        [DisplayName("ID studenta")]
        [CustomRequired]
        public int StudentId { get; set; }

        [DisplayName("Role")]
        [Required]
        public string Role { get; set; }
    }

    /// <summary>
    /// Response: Student minimal 
    /// </summary>
    public class StudentMinRes
    {
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

        public StudentMinRes(Student student)
        {
            StudentId = student.StudentId;
            FirstName = student.FirstName;
            LastName = student.LastName;
        }
    }

    /// <summary>
    /// Response: Student with Address and activated
    /// </summary>
    public class StudentWithAdActivRes
    {
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressRes Address { get; set; }
        public bool Activated { get; set; }

        public StudentWithAdActivRes(Student student)
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
    /// Response: Student with Address
    /// </summary>
    public class StudentRes
    {
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressRes Address { get; set; }

        public StudentRes(Student student)
        {
            StudentId = student.StudentId;
            Email = student.User.Email;
            FirstName = student.FirstName;
            LastName = student.LastName;
            DateOfBirth = student.DateOfBirth;
            Address = new AddressRes(student.Address);
        }
    }
}
