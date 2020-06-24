using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    [Table("User")]
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Student Student { get; set; }
        public int StudentId { get; set; }

        public Operator Operator { get; set; }
        public int OperatorId { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}
