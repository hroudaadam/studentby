using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    /// <summary>
    /// User - entity
    /// </summary>
    [Table("User")]
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Role { get; set; }

        public Student Student { get; set; }
        public int? StudentId { get; set; }

        public Customer Customer { get; set; }
        public int? CustomerId { get; set; }
    }
}
