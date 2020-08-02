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
    public interface IJobOfferService
    {
        Task<JobCreateResponse> CreateJobOfferAsync(JobCreateRequest model, int userId);
        Task<IEnumerable<JobOfferResponse>> GetJobOffersAsync();
        Task<JobOfferDetailResponse> GetJobOfferDetailAsync(int id);
    }

    public class JobOfferService : IJobOfferService
    {
        private readonly StudentbyTestContext _context;

        public JobOfferService(StudentbyTestContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobOfferResponse>> GetJobOffersAsync()
        {
            var jobOffers = await _context.JobOffers
                .ToListAsync();

            List<JobOfferResponse> result = new List<JobOfferResponse>();

            foreach (var jobOffer in jobOffers)
            {
                result.Add(new JobOfferResponse(jobOffer));
            }

            return result;
        }

        public async Task<JobOfferDetailResponse> GetJobOfferDetailAsync(int id)
        {
            var jobOffer = await _context.JobOffers
                .Include(jobOffer => jobOffer.Company)
                .FirstOrDefaultAsync(jobOffer => jobOffer.JobOfferId == id);

            if (jobOffer == null)
            {
                throw new AppException("Not found!");
            }

            return new JobOfferDetailResponse(jobOffer);
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
    }
}
