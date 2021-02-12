using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Helpers;

namespace WebAPI.Models
{
    /// <summary>
    /// Request: JobApplication with JobOffer
    /// </summary>
    public class JobApplicationWithJoReq
    {
        [DisplayName("ID pracovní nabídky")]
        [CustomRequired]
        public int JobOfferId { get; set; }
    }

    /// <summary>
    /// Requst: JobApplication with state
    /// </summary>
    public class JobApplicationWithStateReq
    {
        [DisplayName("ID přihlášky")]
        [CustomRequired]
        public int JobApplicationId { get; set; }
        [DisplayName("Stav přihlášky")]
        [CustomRequired]
        public string State { get; set; }
    }

    /// <summary>
    /// Response: JobApplication with JobOffer minimal
    /// </summary>
    public class JobApplicationMinWithJoRes
    {
        public int JobApplicationId { get; set; }
        public string State { get; set; }
        public JobOfferMinRes JobOffer { get; set; }

        public JobApplicationMinWithJoRes(JobApplication jobApplication)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            JobOffer = new JobOfferMinRes(jobApplication.JobOffer);
        }
    }

    /// <summary>
    /// Response: JobApplication with JobOffer
    /// </summary>
    public class JobApplicationWithJoRes
    {
        public int JobApplicationId { get; set; }
        public string State { get; set; }
        public JobOfferRes JobOffer { get; set; }

        public JobApplicationWithJoRes(JobApplication jobApplication, int freeSpaces)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            JobOffer = new JobOfferRes(jobApplication.JobOffer, freeSpaces);
        }
    }

    /// <summary>
    /// Response: JobApplication with Student
    /// </summary>
    public class JobApplicationMinWithStudRes
    {
    public int JobApplicationId { get; set; }
    public string State { get; set; }
    public StudentMinRes Student { get; set; }

        public JobApplicationMinWithStudRes(JobApplication jobApplication)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            Student = new StudentMinRes(jobApplication.Student);
        }
    }


    /// <summary>
    /// Response: JobApplication with JobOffer and Student
    /// </summary>
    public class JobApplicationWithJoStudRes
    {
    public int JobApplicationId { get; set; }        
    public string State { get; set; }
    public JobOfferRes JobOffer { get; set; }
    public StudentRes Student { get; set; }

        public JobApplicationWithJoStudRes(JobApplication jobApplication, int freeSpaces)
        {
            JobApplicationId = jobApplication.JobApplicationId;
            State = jobApplication.State;
            JobOffer = new JobOfferRes(jobApplication.JobOffer, freeSpaces);
            Student = new StudentRes(jobApplication.Student);
        }
    }
}
