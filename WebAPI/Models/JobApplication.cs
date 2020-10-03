using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    /// <summary>
    /// Interface for JobApplication detail
    /// </summary>
    public interface IJobApplicationDetail
    {
        int JobApplicationId { get; set; }
        string State { get; set; }
    }

    /// <summary>
    /// Request: JobApplication
    /// </summary>
    public class JobApplicationReq
    {
        [Required]
        public int JobOfferId { get; set; }
    }

    /// <summary>
    /// Requst: JobApplication detail
    /// </summary>
    public class JobApplicationDetailReq
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
    /// Response: JobApplication simple
    /// </summary>
    public class JobApplicationSimpleRes
    {
        public int JobApplicationId { get; set; }
        public string State { get; set; }
        public JobOfferSimpleRes JobOffer { get; set; }

        public JobApplicationSimpleRes(JobApplication jobApplication)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            JobOffer = new JobOfferSimpleRes(jobApplication.JobOffer);
        }
    }

    /// <summary>
    /// Response: JobApplication simple with Student
    /// </summary>
    public class JobApplicationSimpleWithStudRes
    {
        public int JobApplicationId { get; set; }
        public string State { get; set; }
        public StudentNameRes Student { get; set; }

        public JobApplicationSimpleWithStudRes(JobApplication jobApplication)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            Student = new StudentNameRes(jobApplication.Student);
        }
    }

    /// <summary>
    /// Response: JobApplication with JobOffer
    /// </summary>
    public class JobApplicationWithJoRes : IJobApplicationDetail
    {
        public int JobApplicationId { get; set; }
        public string State { get; set; }
        public JobOfferDetailRes JobOffer { get; set; }

        public JobApplicationWithJoRes(JobApplication jobApplication, int freeSpaces)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            JobOffer = new JobOfferDetailRes(jobApplication.JobOffer, freeSpaces);
        }
    }

    /// <summary>
    /// Response: JobApplication with JobOffer and Student
    /// </summary>
    public class JobApplicationWithJoAndStudRes : IJobApplicationDetail
    {
        public int JobApplicationId { get; set; }        
        public string State { get; set; }
        public JobOfferDetailRes JobOffer { get; set; }
        public StudentDetailRes Student { get; set; }

        public JobApplicationWithJoAndStudRes(JobApplication jobApplication, int freeSpaces)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            JobOffer = new JobOfferDetailRes(jobApplication.JobOffer, freeSpaces);
            Student = new StudentDetailRes(jobApplication.Student);
        }
    }

    /// <summary>
    /// Response: JobApplication with Student
    /// </summary>
    public class JobApplicationWithStudRes
    {
        public int JobApplicationId { get; set; }
        public string State { get; set; }
        public StudentDetailRes Student { get; set; }

        public JobApplicationWithStudRes(JobApplication jobApplication)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            Student = new StudentDetailRes(jobApplication.Student);
        }
    }
}
