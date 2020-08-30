using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Models
{
    public class JobOfferDetailStudentResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public double Wage { get; set; }
        public int Spaces { get; set; }
        public int FreeSpaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string GroupName { get; set; }

        public JobOfferDetailStudentResponse(JobOffer job, int freeSpaces)
        {
            Id = job.JobOfferId;
            Title = job.Title;
            Description = job.Description;
            Wage = job.Wage;
            Spaces = job.Spaces;
            FreeSpaces = freeSpaces;
            Start = job.Start;
            End = job.End;
            GroupName = job.Group.Title;            
        }
    }

    public class JobOfferDetailCustomerResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public double Wage { get; set; }
        public int Spaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ICollection<JobApplicationCustomerResponse> ApprovedJobApplications { get; set; }

        public JobOfferDetailCustomerResponse(JobOffer job, ICollection<JobApplication> jobApplications)
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

        private List<JobApplicationCustomerResponse> MapJobApplications(ICollection<JobApplication> jobApplications)
        {
            var output = new List<JobApplicationCustomerResponse>();

            foreach (var jobApplication in jobApplications)
            {
                output.Add(new JobApplicationCustomerResponse(jobApplication));
            }

            return output;
        }
    }

    public class JobOfferResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public double Wage { get; set; }
        public int Spaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }


        public JobOfferResponse(JobOffer job)
        {
            Id = job.JobOfferId;
            Title = job.Title;
            Wage = job.Wage;
            Spaces = job.Spaces;
            Start = job.Start;
            End = job.End;
        }
    }

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
