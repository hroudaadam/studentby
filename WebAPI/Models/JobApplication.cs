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
        public int JobApplicationId { get; set; }
        public string Title { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string State { get; set; }

        public JobApplicationSimpleResponse(JobApplication jobApplication)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            Title = jobApplication.JobOffer.Title;
            Start = jobApplication.JobOffer.Start;
            End = jobApplication.JobOffer.End;
            State = jobApplication.State;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobApplicationDetailResponse
    {
        public int JobApplicationId { get; set; }
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
            JobApplicationId = jobApplication.JobApplicationId;
            Title = jobApplication.JobOffer.Title;
            Description = jobApplication.JobOffer.Description;
            Wage = jobApplication.JobOffer.Wage;
            Spaces = jobApplication.JobOffer.Spaces;
            FreeSpaces = freeSpaces;
            Start = jobApplication.JobOffer.Start;
            End = jobApplication.JobOffer.End;
            GroupName = jobApplication.JobOffer.Group.Name;
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
        public string State { get; set; }

        public JobApplicationResponse(JobApplication jobApplication)
        {
            State = jobApplication.State;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobApplicationStateRequest
    {
        [Required]
        public int JobApplicationId { get; set; }

        [Required]
        public string State { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JobApplicationDetailWithStudentResponse
    {
        public int JobApplicationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public double Wage { get; set; }
        public int Spaces { get; set; }
        public int FreeSpaces { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string GroupName { get; set; }
        public string State { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public JobApplicationDetailWithStudentResponse(JobApplication jobApplication, int freeSpaces)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            Title = jobApplication.JobOffer.Title;
            Description = jobApplication.JobOffer.Description;
            Wage = jobApplication.JobOffer.Wage;
            Spaces = jobApplication.JobOffer.Spaces;
            FreeSpaces = freeSpaces;
            Start = jobApplication.JobOffer.Start;
            End = jobApplication.JobOffer.End;
            GroupName = jobApplication.JobOffer.Group.Name;
            State = jobApplication.State;
            FirstName = jobApplication.Student.FirstName;
            LastName = jobApplication.Student.LastName;
            DateOfBirth = jobApplication.Student.DateOfBirth;
        }
    }
}
