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
        public string Description { get; set; }

        public double Wage { get; set; }
        public int Spaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string CompanyName { get; set; }

        public string State { get; set; }

        public JobApplicationResponse(JobApplication jobApplication)
        {
            Id = jobApplication.JobOffer.JobOfferId;
            Title = jobApplication.JobOffer.Title;
            Description = jobApplication.JobOffer.Description;
            Wage = jobApplication.JobOffer.Wage;
            Spaces = jobApplication.JobOffer.Spaces;
            Start = jobApplication.JobOffer.Start;
            End = jobApplication.JobOffer.End;
            CompanyName = jobApplication.JobOffer.Company.Title;
            State = jobApplication.State;
        }
    }
}
