using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    [Table("JobOffer")]
    public class JobOffer
    {
        public int JobOfferId { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public ICollection<Registration> Registrations { get; set; }
    }
}
