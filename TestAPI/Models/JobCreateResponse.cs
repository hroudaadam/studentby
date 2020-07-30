using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;

namespace TestAPI.Models
{
    public class JobCreateResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public JobCreateResponse(JobOffer job)
        {
            Title = job.Title;
            Description = job.Description;
        }
    }
}
