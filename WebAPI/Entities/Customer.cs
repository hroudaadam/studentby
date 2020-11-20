using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    /// <summary>
    /// Customer - entity
    /// </summary>
    [Table("Customer")]
    public class Customer
    {
        public int CustomerId { get; set; }
        public int GroupId { get; set; }

        public Group Group { get; set; }
        public User User { get; set; }
    }
}
