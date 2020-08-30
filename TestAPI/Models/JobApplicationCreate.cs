using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;

namespace TestAPI.Models
{
    public class JobApplicationCreateRequest
    {
        [Required]
        public int JobOfferId { get; set; }
    }

    public class JobApplicationCreateResponse
    {
        public string State { get; set; }

        public JobApplicationCreateResponse(JobApplication jobApplication)
        {
            State = jobApplication.State;
        }
    }
}
