using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Models
{
    public interface IJobOfferDetail
    {
        int JobOfferId { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        double Wage { get; set; }
        int Spaces { get; set; }
        int FreeSpaces { get; set; }

        DateTime Start { get; set; }
        DateTime End { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobOfferDetailResponse: IJobOfferDetail
    {
        public int JobOfferId { get; set; }
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
            JobOfferId = job.JobOfferId;
            Title = job.Title;
            Description = job.Description;
            Wage = job.Wage;
            Spaces = job.Spaces;
            FreeSpaces = freeSpaces;
            Start = job.Start;
            End = job.End;
            GroupName = job.Group.Name;            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobOfferDetailWithStudentsResponse: IJobOfferDetail
    {
        public int JobOfferId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public double Wage { get; set; }
        public int Spaces { get; set; }
        public int FreeSpaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ICollection<StudentSimpleResponse> Students { get; set; }

        public JobOfferDetailWithStudentsResponse(JobOffer job, ICollection<JobApplication> jobApplications, int freeSpaces)
        {
            JobOfferId = job.JobOfferId;
            Title = job.Title;
            Description = job.Description;
            Wage = job.Wage;
            Spaces = job.Spaces;
            FreeSpaces = freeSpaces;
            Start = job.Start;
            End = job.End;
            Students = MapJobApplications(jobApplications);
        }

        private List<StudentSimpleResponse> MapJobApplications(ICollection<JobApplication> jobApplications)
        {
            var output = new List<StudentSimpleResponse>();

            foreach (var jobApplication in jobApplications)
            {
                output.Add(new StudentSimpleResponse(jobApplication.Student));
            }

            return output;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobOfferSimpleResponse
    {
        public int JobOfferId { get; set; }
        public string Title { get; set; }

        public double Wage { get; set; }
        public int Spaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }


        public JobOfferSimpleResponse(JobOffer job)
        {
            JobOfferId = job.JobOfferId;
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
