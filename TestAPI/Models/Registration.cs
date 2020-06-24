using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    [Table("Registration")]
    public class Registration
    {
        public int RegistrationId { get; set; }

        public Student Student { get; set; }
        public int StudentId { get; set; }
    }
}
