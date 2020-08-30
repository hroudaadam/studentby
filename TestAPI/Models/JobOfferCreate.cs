using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;

namespace TestAPI.Models
{
    public class JobOfferCreateRequest
    {
        [Required]
        [MinLength(6, ErrorMessage = "Min delka je 6")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Wage { get; set; }
        [Required]
        public int Spaces { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
    }

    public class JobOfferCreateResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public JobOfferCreateResponse(JobOffer job)
        {
            Title = job.Title;
            Description = job.Description;
        }
    }
}
