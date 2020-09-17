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
        Task<JobOfferResponse> CreateAsync(JobOfferRequest model, int userId);
        Task<bool> DeleteAsync(int jobOfferId, int userId);
        Task<IEnumerable<JobOfferSimpleResponse>> GetListStudentAsync(int userId);        
        Task<IEnumerable<JobOfferSimpleResponse>> GetListCustomerAsync(int userId);
        Task<JobOfferDetailResponse> GetDetailStudentAsync(int id);
        Task<JobOfferDetailWithStudentsResponse> GetDetailCustomerAsync(int id, int userId);
        Task<int> GetFreeSpacesAsync(int jobOfferId);
    }

    public class JobOfferService : IJobOfferService
    {
        private readonly StudentbyContext _context;

        public JobOfferService(StudentbyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobOfferSimpleResponse>> GetListStudentAsync(int userId)
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

        public async Task<IEnumerable<JobOfferSimpleResponse>> GetListCustomerAsync(int userId)
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

        public async Task<JobOfferDetailResponse> GetDetailStudentAsync(int jobOfferId)
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

        public async Task<JobOfferDetailWithStudentsResponse> GetDetailCustomerAsync(int jobOfferId, int userId)
        {
            var user = await _context.Users
                .Include(us => us.Customer)
                .FirstOrDefaultAsync(us => us.UserId == userId);

            var jobOffer = await _context.JobOffers
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            // kontrola, zda nabídka existuje
            if (jobOffer == null)
            {
                return null;
            }

            // kontrola, zda je nabídka od skupiny, které uživatel náleží
            if (jobOffer.GroupId != user.Customer.GroupId)
            {
                throw new StudentbyException("Nabídka nepatří k dané skupině");
            }

            // přijaté přihlášky k nabídce
            var jobApplications = await _context.JobApplications
                .Where(ja => (ja.JobOfferId == jobOffer.JobOfferId) && (ja.State == JobApplicationState.Approved))
                .Include(ja => ja.Student)
                .ToListAsync();

            int freeSpaces = await GetFreeSpacesAsync(jobOffer.JobOfferId);
            return new JobOfferDetailWithStudentsResponse(jobOffer, jobApplications, freeSpaces);
        }

        public async Task<JobOfferResponse> CreateAsync(JobOfferRequest model, int userId)
        {
            User user = await _context.Users
                .Include(us => us.Customer)
                    .ThenInclude(em => em.Group)
                .FirstOrDefaultAsync(us => us.UserId == userId);

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

        public async Task<bool> DeleteAsync(int jobOfferId, int userId)
        {
            var user = await _context.Users
                .Include(us => us.Customer)
                .FirstOrDefaultAsync(us => us.UserId == userId);

            var jobOffer = await _context.JobOffers
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            // kontrola, zda nabídka existuje
            if (jobOffer == null)
            {
                return false;
            }

            // kontrola, zda je nabídka od skupiny, které uživatel náleží
            if (jobOffer.GroupId != user.Customer.GroupId)
            {
                throw new StudentbyException("Nabídka nepatří k dané skupině");
            }

            if (jobOffer.Start.Date < DateTime.UtcNow.Date)
            {
                throw new StudentbyException("Nabídka již započala");
            }

            _context.JobOffers.Remove(jobOffer);
            await _context.SaveChangesAsync();
            return true;
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
