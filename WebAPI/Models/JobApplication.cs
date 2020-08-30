using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class JobApplicationSimpleResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string State { get; set; }

        public JobApplicationSimpleResponse(JobApplication jobApplication)
        {
            Id = jobApplication.JobApplicationId;
            Title = jobApplication.JobOffer.Title;
            Start = jobApplication.JobOffer.Start;
            End = jobApplication.JobOffer.End;
            State = jobApplication.State;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ApplicantNameResponse
    {
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

        public ApplicantNameResponse(JobApplication jobApplication)
        {
            StudentFirstName = jobApplication.Student.FirstName;
            StudentLastName = jobApplication.Student.LastName;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobApplicationDetailResponse
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
        public string State { get; set; }

        public JobApplicationDetailResponse(JobApplication jobApplication, int freeSpaces)
        {
            Id = jobApplication.JobApplicationId;
            Title = jobApplication.JobOffer.Title;
            Description = jobApplication.JobOffer.Description;
            Wage = jobApplication.JobOffer.Wage;
            Spaces = jobApplication.JobOffer.Spaces;
            FreeSpaces = freeSpaces;
            Start = jobApplication.JobOffer.Start;
            End = jobApplication.JobOffer.End;
            GroupName = jobApplication.JobOffer.Group.Title;
            State = jobApplication.State;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobApplicationRequest
    {
        [Required]
        public int JobOfferId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobApplicationResponse
    {
        public int JobOfferId { get; set; }
        public string State { get; set; }

        public JobApplicationResponse(JobApplication jobApplication)
        {
            JobOfferId = jobApplication.JobOfferId;
            State = jobApplication.State;
        }
    }
}
