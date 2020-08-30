using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        Task<JobOfferCreateResponse> CreateJobOfferAsync(JobOfferCreateRequest model, int userId);
        Task<IEnumerable<JobOfferResponse>> GetStudentJobOffersAsync(int userId);        
        Task<IEnumerable<JobOfferResponse>> GetCompanyJobOffersAsync(int userId);

        Task<JobOfferDetailStudentResponse> GetJobOfferDetailStudentAsync(int id);
        Task<JobOfferDetailEmployeeResponse> GetJobOfferDetailEmployeeAsync(int id, int userId);

        Task<int> GetFreeSpacesAsync(int jobOfferId);
    }

    public class JobOfferService : IJobOfferService
    {
        private readonly StudentbyTestContext _context;

        public JobOfferService(StudentbyTestContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobOfferResponse>> GetStudentJobOffersAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            var jobOffers = await _context.JobOffers
                .Where(jo => jo.Start.Date > DateTime.UtcNow.Date)
                .Include(jo => jo.JobApplications)
                .Where(jo => jo.JobApplications
                    .All(ja => ja.StudentId != user.StudentId))
                .ToListAsync();

            List<JobOfferResponse> result = new List<JobOfferResponse>();

            foreach (var jobOffer in jobOffers)
            {
                result.Add(new JobOfferResponse(jobOffer));
            }

            return result;
        }

        public async Task<IEnumerable<JobOfferResponse>> GetCompanyJobOffersAsync(int userId)
        {
            User user = await _context.Users
                .Include(us => us.Employee)
                .FirstOrDefaultAsync(us => us.UserId == userId);

            int companyId = user.Employee.CompanyId;

            var jobOffers = await _context.JobOffers
                .Where(jo => jo.CompanyId == companyId)
                .ToListAsync();

            List<JobOfferResponse> result = new List<JobOfferResponse>();

            foreach (var jobOffer in jobOffers)
            {
                result.Add(new JobOfferResponse(jobOffer));
            }

            return result;
        }

        public async Task<JobOfferDetailStudentResponse> GetJobOfferDetailStudentAsync(int jobOfferId)
        {
            var jobOffer = await _context.JobOffers
                .Include(jo => jo.Company)
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            if (jobOffer == null)
            {
                return null;
            }

            int freeSpaces = await GetFreeSpacesAsync(jobOfferId);

            return new JobOfferDetailStudentResponse(jobOffer, freeSpaces);
        }

        public async Task<JobOfferDetailEmployeeResponse> GetJobOfferDetailEmployeeAsync(int jobOfferId, int userId)
        {
            var user = await _context.Users
                .Include(us => us.Employee)
                .FirstOrDefaultAsync(us => us.UserId == userId);

            var jobOffer = await _context.JobOffers
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            if (jobOffer == null)
            {
                return null;
            }

            if (jobOffer.CompanyId != user.Employee.CompanyId)
            {
                throw new StudentbyException("Nabídka nepatří k dané skupině");
            }

            var jobApplications = await _context.JobApplications
                .Where(ja => (ja.JobOfferId == jobOffer.JobOfferId) && (ja.State == State.Approved))
                .Include(ja => ja.Student)
                .ToListAsync();              

            return new JobOfferDetailEmployeeResponse(jobOffer, jobApplications);
        }

        public async Task<JobOfferCreateResponse> CreateJobOfferAsync(JobOfferCreateRequest model, int userId)
        {
            User user = await _context.Users
                .Include(us => us.Employee)
                    .ThenInclude(em => em.Company)
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
                Company = user.Employee.Company
            };

            _context.JobOffers.Add(jobOffer);
            await _context.SaveChangesAsync();

            return new JobOfferCreateResponse(jobOffer);
        }
        
        public async Task<int> GetFreeSpacesAsync(int jobOfferId)
        {
            var jobOffer = await _context.JobOffers
                .Include(jo => jo.JobApplications)
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            int occupied = jobOffer.JobApplications.Count(ja => ja.State == State.Approved);
            return jobOffer.Spaces - occupied;
        }
    }
}
