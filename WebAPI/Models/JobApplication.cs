using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    /// <summary>
    /// Request: JobApplication with JobOffer
    /// </summary>
    public class JobApplicationWithJoReq
    {
        [Required]
        public int JobOfferId { get; set; }
    }

    /// <summary>
    /// Requst: JobApplication with state
    /// </summary>
    public class JobApplicationWithStateReq
    {
        [Required]
        public int JobApplicationId { get; set; }
        [Required]
        public string State { get; set; }
    }    

    /// <summary>
    /// Response: JobApplication
    /// </summary>
    public class JobApplicationRes
    {
        public int JobApplicationId { get; set; }
        public string State { get; set; }

        public JobApplicationRes(JobApplication jobApplication)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
        }
    }

    /// <summary>
    /// Response: JobApplication with simple JobOffer
    /// </summary>
    public class JobApplicationWithSimpleJoRes
    {
        public int JobApplicationId { get; set; }
        public string State { get; set; }
        public JobOfferSimpleRes JobOffer { get; set; }

        public JobApplicationWithSimpleJoRes(JobApplication jobApplication)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            JobOffer = new JobOfferSimpleRes(jobApplication.JobOffer);
        }
    }

    /// <summary>
    /// Response: JobApplication with Student
    /// </summary>
    public class JobApplicationWithStudRes
    {
        public int JobApplicationId { get; set; }
        public string State { get; set; }
        public StudentNameRes Student { get; set; }

        public JobApplicationWithStudRes(JobApplication jobApplication)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            Student = new StudentNameRes(jobApplication.Student);
        }
    }

    /// <summary>
    /// Response: JobApplication with JobOffer
    /// </summary>
    public class JobApplicationWithJoRes
    {
        public int JobApplicationId { get; set; }
        public string State { get; set; }
        public JobOfferWithGrAdRes JobOffer { get; set; }

        public JobApplicationWithJoRes(JobApplication jobApplication, int freeSpaces)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            JobOffer = new JobOfferWithGrAdRes(jobApplication.JobOffer, freeSpaces);
        }
    }

    /// <summary>
    /// Response: JobApplication with JobOffer and Student
    /// </summary>
    public class JobApplicationWithJoStudRes
    {
        public int JobApplicationId { get; set; }        
        public string State { get; set; }
        public JobOfferWithGrAdRes JobOffer { get; set; }
        public StudentWithAdRes Student { get; set; }

        public JobApplicationWithJoStudRes(JobApplication jobApplication, int freeSpaces)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            JobOffer = new JobOfferWithGrAdRes(jobApplication.JobOffer, freeSpaces);
            Student = new StudentWithAdRes(jobApplication.Student);
        }
    }
}
