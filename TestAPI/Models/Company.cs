using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    [Table("Company")]
    public class Company
    {
        public int CompanyId { get; set; }

        public User User { get; set; }
    }
}
