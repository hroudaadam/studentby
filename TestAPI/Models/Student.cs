using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    [Table("Student")]
    public class Student
    {
        public int StudentId { get; set; }

        public User User { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}
