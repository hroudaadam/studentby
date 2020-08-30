using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;

namespace TestAPI.Models
{
    public class JobOfferDetailEmployeeResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public double Wage { get; set; }
        public int Spaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ICollection<JobApplicationEmployeeResponse> ApprovedJobApplications { get; set; }

        public JobOfferDetailEmployeeResponse(JobOffer job, ICollection<JobApplication> jobApplications)
        {
            Id = job.JobOfferId;
            Title = job.Title;
            Description = job.Description;
            Wage = job.Wage;
            Spaces = job.Spaces;
            Start = job.Start;
            End = job.End;
            ApprovedJobApplications = MapJobApplications(jobApplications);
        }

        private List<JobApplicationEmployeeResponse> MapJobApplications(ICollection<JobApplication> jobApplications)
        {
            var output = new List<JobApplicationEmployeeResponse>();

            foreach (var jobApplication in jobApplications)
            {
                output.Add(new JobApplicationEmployeeResponse(jobApplication));
            }

            return output;
        }
    }
}