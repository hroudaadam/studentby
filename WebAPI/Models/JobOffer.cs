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
        DateTime Start { get; set; }
        DateTime End { get; set; }
        public AddressRes Address { get; set; }
    }

    /// <summary>
    /// Request: JobOffer
    /// </summary>
    public class JobOfferReq
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
        [Required]
        public AddressRequest Address { get; set; }
    }

    /// <summary>
    /// Response: JobOffer
    /// </summary>
    public class JobOfferRes
    {
        public int JobOfferId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Wage { get; set; }
        public int Spaces { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public JobOfferRes(JobOffer job)
        {
            JobOfferId = job.JobOfferId;
            Title = job.Title;
            Description = job.Description;
            Wage = job.Wage;
            Spaces = job.Spaces;
            Start = job.Start;
            End = job.End;
        }
    }

    /// <summary>
    /// Response: JobOffer detail
    /// </summary>
    public class JobOfferDetailRes: IJobOfferDetail
    {
        public int JobOfferId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Wage { get; set; }
        public int Spaces { get; set; }
        public int FreeSpaces { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public GroupRes Group { get; set; }
        public AddressRes Address { get; set; }

        public JobOfferDetailRes(JobOffer jobOffer, int freeSpaces)
        {
            JobOfferId = jobOffer.JobOfferId;
            Title = jobOffer.Title;
            Description = jobOffer.Description;
            Wage = jobOffer.Wage;
            Spaces = jobOffer.Spaces;
            FreeSpaces = freeSpaces;
            Start = jobOffer.Start;
            End = jobOffer.End;
            Group = new GroupRes(jobOffer.Group);
            Address = new AddressRes(jobOffer.Address);
        }
    }

    /// <summary>
    /// Response: JobOffer with JobApplications
    /// </summary>
    public class JobOfferWithJasRes : IJobOfferDetail
    {
        public int JobOfferId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Wage { get; set; }
        public int Spaces { get; set; }
        public int FreeSpaces { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public GroupRes Group { get; set; }
        public AddressRes Address { get; set; }
        public ICollection<JobApplicationSimpleWithStudRes> JobApplications { get; set; }

        public JobOfferWithJasRes(JobOffer jobOffer, ICollection<JobApplication> jobApplications, int freeSpaces)
        {
            JobOfferId = jobOffer.JobOfferId;
            Title = jobOffer.Title;
            Description = jobOffer.Description;
            Wage = jobOffer.Wage;
            Spaces = jobOffer.Spaces;
            FreeSpaces = freeSpaces;
            Start = jobOffer.Start;
            End = jobOffer.End;
            JobApplications = MapJobApplications(jobApplications);
            Address = new AddressRes(jobOffer.Address);
            Group = new GroupRes(jobOffer.Group);
        }

        private List<JobApplicationSimpleWithStudRes> MapJobApplications(ICollection<JobApplication> jobApplications)
        {
            var output = new List<JobApplicationSimpleWithStudRes>();

            foreach (var jobApplication in jobApplications)
            {
                output.Add(new JobApplicationSimpleWithStudRes(jobApplication));
            }

            return output;
        }
    }

    /// <summary>
    /// Response: JobOffer simple
    /// </summary>
    public class JobOfferSimpleRes
    {
        public int JobOfferId { get; set; }
        public string Title { get; set; }
        public double Wage { get; set; }
        public int Spaces { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public JobOfferSimpleRes(JobOffer jobOffer)
        {
            JobOfferId = jobOffer.JobOfferId;
            Title = jobOffer.Title;
            Wage = jobOffer.Wage;
            Spaces = jobOffer.Spaces;
            Start = jobOffer.Start;
            End = jobOffer.End;
        }
    }
}
