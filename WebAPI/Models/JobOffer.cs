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
        public AddressReq Address { get; set; }
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
        public int? FreeSpaces { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public GroupRes Group { get; set; }
        public AddressRes Address { get; set; }

        public JobOfferRes(JobOffer jobOffer, int? freeSpaces=null)
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
    /// Response: JobOffer with JobAppliactions
    /// </summary>
    public class JobOfferWithJasRes
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
        public ICollection<JobApplicationMinWithStudRes> JobApplications { get; set; }

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

        private List<JobApplicationMinWithStudRes> MapJobApplications(ICollection<JobApplication> jobApplications)
        {
            var output = new List<JobApplicationMinWithStudRes>();

            foreach (var jobApplication in jobApplications)
            {
                output.Add(new JobApplicationMinWithStudRes(jobApplication));
            }

            return output;
        }
    }

    /// <summary>
    /// Response: JobOffer minimal
    /// </summary>
    public class JobOfferMinRes
    {
        public int JobOfferId { get; set; }
        public string Title { get; set; }
        public GroupRes Group { get; set; }
        public AddressRes Address { get; set; }
        public double Wage { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public JobOfferMinRes(JobOffer jobOffer)
        {
            JobOfferId = jobOffer.JobOfferId;
            Title = jobOffer.Title;
            Wage = jobOffer.Wage;
            Group = new GroupRes(jobOffer.Group);
            Address = new AddressRes(jobOffer.Address);
            Start = jobOffer.Start;
            End = jobOffer.End;
        }
    }
}
