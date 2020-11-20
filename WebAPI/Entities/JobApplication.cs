using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    /// <summary>
    /// JobApplication - entity
    /// </summary>
    [Table("JobApplication")]
    public class JobApplication
    {
        public int JobApplicationId { get; set; }
        [Required]
        public string State { get; set; }

        public int StudentId { get; set; }
        public int JobOfferId { get; set; }

        public Student Student { get; set; }
        public JobOffer JobOffer { get; set; }
    }
}
