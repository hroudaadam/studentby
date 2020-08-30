using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
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

    public class JobApplicationCustomerResponse
    {
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

        public JobApplicationCustomerResponse(JobApplication jobApplication)
        {
            StudentFirstName = jobApplication.Student.FirstName;
            StudentLastName = jobApplication.Student.LastName;
        }
    }

    public class JobApplicationDetailStudentResponse
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

        public JobApplicationDetailStudentResponse(JobApplication jobApplication, int freeSpaces)
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

    public class JobApplicationCreateRequest
    {
        [Required]
        public int JobOfferId { get; set; }
    }

    public class JobApplicationCreateResponse
    {
        public string State { get; set; }

        public JobApplicationCreateResponse(JobApplication jobApplication)
        {
            State = jobApplication.State;
        }
    }
}
