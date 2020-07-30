using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;
using TestAPI.Helpers;
using TestAPI.Models;

namespace TestAPI.Services
{
    public interface IJobService
    {
        Task<JobCreateResponse> CreateJobOfferAsync(JobCreateRequest model, int userId);
        Task<JobApplicationCreateResponse> ApplicationCreateAsync(JobApplicationCreateRequest model, int userId);
    }

    public class JobService : IJobService
    {
        private readonly StudentbyTestContext _context;

        public JobService(StudentbyTestContext context)
        {
            _context = context;
        }

        public async Task<JobCreateResponse> CreateJobOfferAsync(JobCreateRequest model, int userId)
        {
            User user = await _context.Users
                .Where(user => user.UserId == userId)
                .Include(user => user.Employee)
                    .ThenInclude(employee => employee.Company)
                .FirstOrDefaultAsync();

            JobOffer jobOffer = new JobOffer
            {
                Title = model.Title,
                Description = model.Description,
                Wage = model.Wage,
                Spaces = model.Spaces,
                Start = model.Start,
                End = model.End,                
                Company = user.Employee.Company
            };

            _context.JobOffers.Add(jobOffer);
            await _context.SaveChangesAsync();

            return new JobCreateResponse(jobOffer);
        }

        public async Task<JobApplicationCreateResponse> ApplicationCreateAsync(JobApplicationCreateRequest model, int userId)
        {
            JobOffer jobOffer = await _context.JobOffers.FirstOrDefaultAsync(x => x.JobOfferId == model.JobOfferId);

            Student student = await _context.Students
                .Include(student => student.User)
                .Where(student => student.User.UserId == userId)
                .FirstOrDefaultAsync();

            JobApplication jobApplication = new JobApplication
            {
                State = State.Pending,
                Student = student,
                JobOffer = jobOffer
            };

            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();

            return new JobApplicationCreateResponse(jobApplication);
        }

    }
}
