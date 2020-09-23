using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    [Table("Student")]
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Address Address { get; set; }
        public int? AddressId { get; set; }

        public User User { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
