using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Entities
{
    [Table("Company")]
    public class Company
    {
        public int CompanyId { get; set; }
        public string Title { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<JobOffer> JobOffers { get; set; }
    }
}
