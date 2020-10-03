using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    /// <summary>
    /// JobOffer - entity
    /// </summary>
    [Table("JobOffer")]
    public class JobOffer
    {
        public int JobOfferId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public double Wage { get; set; }

        public int Spaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Address Address { get; set; }
        public int? AddressId { get; set; }

        public Group Group { get; set; }
        public int GroupId { get; set; }

        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
