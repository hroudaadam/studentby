using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    /// <summary>
    /// Group - entity
    /// </summary>
    [Table("Group")]
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<JobOffer> JobOffers { get; set; }
    }
}
