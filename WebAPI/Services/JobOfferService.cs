using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IJobOfferService
    {
        Task<JobOfferResponse> CreateJobOfferAsync(JobOfferRequest model, int userId);
        Task<IEnumerable<JobOfferSimpleResponse>> GetJobOffersStudentAsync(int userId);        
        Task<IEnumerable<JobOfferSimpleResponse>> GetJobOffersCustomerAsync(int userId);

        Task<JobOfferDetailResponse> GetJobOfferDetailStudentAsync(int id);
        Task<JobOfferDetailWithApplicationsResponse> GetJobOfferDetailCustomerAsync(int id, int userId);

        Task<int> GetFreeSpacesAsync(int jobOfferId);
    }

    public class JobOfferService : IJobOfferService
    {
        private readonly StudentbyContext _context;

        public JobOfferService(StudentbyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobOfferSimpleResponse>> GetJobOffersStudentAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            var jobOffers = await _context.JobOffers
                .Where(jo => jo.Start.Date > DateTime.UtcNow.Date)
                .Include(jo => jo.JobApplications)
                .Where(jo => jo.JobApplications
                    .All(ja => ja.StudentId != user.StudentId))
                .ToListAsync();

            List<JobOfferSimpleResponse> result = new List<JobOfferSimpleResponse>();

            foreach (var jobOffer in jobOffers)
            {
                result.Add(new JobOfferSimpleResponse(jobOffer));
            }

            return result;
        }

        public async Task<IEnumerable<JobOfferSimpleResponse>> GetJobOffersCustomerAsync(int userId)
        {
            User user = await _context.Users
                .Include(us => us.Customer)
                .FirstOrDefaultAsync(us => us.UserId == userId);

            int groupId = user.Customer.GroupId;

            var jobOffers = await _context.JobOffers
                .Where(jo => jo.GroupId == groupId)
                .ToListAsync();

            List<JobOfferSimpleResponse> result = new List<JobOfferSimpleResponse>();

            foreach (var jobOffer in jobOffers)
            {
                result.Add(new JobOfferSimpleResponse(jobOffer));
            }

            return result;
        }

        public async Task<JobOfferDetailResponse> GetJobOfferDetailStudentAsync(int jobOfferId)
        {
            var jobOffer = await _context.JobOffers
                .Include(jo => jo.Group)
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            if (jobOffer == null)
            {
                return null;
            }

            int freeSpaces = await GetFreeSpacesAsync(jobOfferId);

            return new JobOfferDetailResponse(jobOffer, freeSpaces);
        }

        public async Task<JobOfferDetailWithApplicationsResponse> GetJobOfferDetailCustomerAsync(int jobOfferId, int userId)
        {
            var user = await _context.Users
                .Include(us => us.Customer)
                .FirstOrDefaultAsync(us => us.UserId == userId);

            var jobOffer = await _context.JobOffers
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            if (jobOffer == null)
            {
                return null;
            }

            if (jobOffer.GroupId != user.Customer.GroupId)
            {
                throw new StudentbyException("Nabídka nepatří k dané skupině");
            }

            var jobApplications = await _context.JobApplications
                .Where(ja => (ja.JobOfferId == jobOffer.JobOfferId) && (ja.State == JobApplicationState.Approved))
                .Include(ja => ja.Student)
                .ToListAsync();              

            return new JobOfferDetailWithApplicationsResponse(jobOffer, jobApplications);
        }

        public async Task<JobOfferResponse> CreateJobOfferAsync(JobOfferRequest model, int userId)
        {
            User user = await _context.Users
                .Include(us => us.Customer)
                    .ThenInclude(em => em.Group)
                .FirstOrDefaultAsync(us => us.UserId == userId);

            if (user == null)
            {
                throw new StudentbyException("Uživatel nenalezen");
            }

            JobOffer jobOffer = new JobOffer
            {
                Title = model.Title,
                Description = model.Description,
                Wage = model.Wage,
                Spaces = model.Spaces,
                Start = model.Start.ToUniversalTime(),
                End = model.End.ToUniversalTime(),                
                Group = user.Customer.Group
            };

            _context.JobOffers.Add(jobOffer);
            await _context.SaveChangesAsync();

            return new JobOfferResponse(jobOffer);
        }
        
        public async Task<int> GetFreeSpacesAsync(int jobOfferId)
        {
            var jobOffer = await _context.JobOffers
                .Include(jo => jo.JobApplications)
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            int occupied = jobOffer.JobApplications.Count(ja => ja.State == JobApplicationState.Approved);
            return jobOffer.Spaces - occupied;
        }
    }
}
