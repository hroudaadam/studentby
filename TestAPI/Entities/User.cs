using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Entities
{
    [Table("User")]
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool Verified { get; set; }

        public string Role { get; set; }

        public Student Student { get; set; }
        public int? StudentId { get; set; }

        public Operator Operator { get; set; }
        public int? OperatorId { get; set; }

        public Employee Employee { get; set; }
        public int? EmployeeId { get; set; }
    }
}
