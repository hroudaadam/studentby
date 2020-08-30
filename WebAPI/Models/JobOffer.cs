using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class JobOfferDetailResponse
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

        public JobOfferDetailResponse(JobOffer job, int freeSpaces)
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

    /// <summary>
    /// 
    /// </summary>
    public class JobOfferDetailWithApplicationsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public double Wage { get; set; }
        public int Spaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ICollection<ApplicantNameResponse> ApprovedJobApplications { get; set; }

        public JobOfferDetailWithApplicationsResponse(JobOffer job, ICollection<JobApplication> jobApplications)
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

        private List<ApplicantNameResponse> MapJobApplications(ICollection<JobApplication> jobApplications)
        {
            var output = new List<ApplicantNameResponse>();

            foreach (var jobApplication in jobApplications)
            {
                output.Add(new ApplicantNameResponse(jobApplication));
            }

            return output;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobOfferSimpleResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public double Wage { get; set; }
        public int Spaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }


        public JobOfferSimpleResponse(JobOffer job)
        {
            Id = job.JobOfferId;
            Title = job.Title;
            Wage = job.Wage;
            Spaces = job.Spaces;
            Start = job.Start;
            End = job.End;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobOfferRequest
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

    /// <summary>
    /// 
    /// </summary>
    public class JobOfferResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public JobOfferResponse(JobOffer job)
        {
            Title = job.Title;
            Description = job.Description;
        }
    }
}
