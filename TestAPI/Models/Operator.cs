using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    [Table("Operator")]
    public class Operator
    {
        public int OperatorId { get; set; }

        public User User { get; set; }
    }
}
