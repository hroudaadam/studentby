using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;

namespace TestAPI.Models
{
    public class JobApplicationResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string State { get; set; }

        public JobApplicationResponse(JobApplication jobApplication)
        {
            Id = jobApplication.JobApplicationId;
            Title = jobApplication.JobOffer.Title;
            Start = jobApplication.JobOffer.Start;
            End = jobApplication.JobOffer.End;
            State = jobApplication.State;
        }
    }
}
