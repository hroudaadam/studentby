﻿using Microsoft.EntityFrameworkCore;
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
        Task<IEnumerable<JobOfferMinRes>> GetListAsync();
        Task<IEnumerable<JobOfferMinRes>> GetListCustomerAsync(int userId);
        Task<JobOfferRes> CreateAsync(JobOfferReq model, int userId);
        Task<bool> DeleteAsync(int jobOfferId, int userId);
        Task<JobOfferRes> GetDetailStudentAsync(int id);
        Task<JobOfferRes> GetDetailCustomerAsync(int id, int userId);
        Task<JobOfferWithJasRes> GetDetailOperatorAsync(int jobOfferId);
        Task<int> GetFreeSpacesAsync(int jobOfferId);
    }

    /// <summary>
    /// Service for JobOffer operations
    /// </summary>
    public class JobOfferService : IJobOfferService
    {
        private readonly StudentbyContext _context;
        private readonly IAddressService _addressService;

        public JobOfferService(StudentbyContext context, IAddressService addressService)
        {
            _context = context;
            _addressService = addressService;
        }

        /// <summary>
        /// Get list of JobOffers
        /// </summary>
        /// <returns>List of JobOffer DTOs</returns>
        public async Task<IEnumerable<JobOfferMinRes>> GetListAsync()
        {
            var jobOffers = await _context.JobOffers
                .Include(jo => jo.Group)
                .Include(jo => jo.Address)
                .ToListAsync();

            List<JobOfferMinRes> result = new List<JobOfferMinRes>();
            foreach (var jobOffer in jobOffers)
            {
                result.Add(new JobOfferMinRes(jobOffer));
            }
            return result;
        }

        /// <summary>
        /// Get list of JobOffers (as Customer)
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>List of JobOffer DTOs</returns>
        public async Task<IEnumerable<JobOfferMinRes>> GetListCustomerAsync(int userId)
        {
            User user = await _context.Users
                .Include(us => us.Customer)
                .FirstOrDefaultAsync(us => us.UserId == userId);

            int groupId = user.Customer.GroupId;

            // get job offers only for current group
            var jobOffers = await _context.JobOffers
                .Include(jo => jo.Group)
                .Include(jo => jo.Address)
                .Where(jo => jo.GroupId == groupId)
                .ToListAsync();

            List<JobOfferMinRes> result = new List<JobOfferMinRes>();
            foreach (var jobOffer in jobOffers)
            {
                result.Add(new JobOfferMinRes(jobOffer));
            }
            return result;
        }

        /// <summary>
        /// Get detail of JobOffer
        /// </summary>
        /// <param name="jobOfferId">JobOffer ID</param>
        /// <returns>JobOffer DTO</returns>
        public async Task<JobOfferRes> GetDetailStudentAsync(int jobOfferId)
        {
            var jobOffer = await _context.JobOffers
                .Include(jo => jo.Address)
                .Include(jo => jo.Group)
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            // job offer not found
            if (jobOffer == null)
            {
                return null;
            }

            int freeSpaces = await GetFreeSpacesAsync(jobOfferId);
            return new JobOfferRes(jobOffer, freeSpaces);
        }

        /// <summary>
        /// Get detail of JobOffer (as Customer)
        /// </summary>
        /// <param name="jobOfferId">JobOffer ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>JobOffer DTO</returns>
        public async Task<JobOfferRes> GetDetailCustomerAsync(int jobOfferId, int userId)
        {
            var user = await _context.Users
                .Include(us => us.Customer)
                .FirstOrDefaultAsync(us => us.UserId == userId);

            var jobOffer = await _context.JobOffers
                .Include(jo => jo.Address)
                .Include(jo => jo.Group)
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            // job offer not found
            if (jobOffer == null)
            {
                return null;
            }

            // check if job offer belongs to current group
            if (jobOffer.GroupId != user.Customer.GroupId)
            {
                throw new AppLogicException("Nabídka nepatří k dané skupině");
            }

            int freeSpaces = await GetFreeSpacesAsync(jobOffer.JobOfferId);
            return new JobOfferRes(jobOffer, freeSpaces);
        }

        /// <summary>
        /// Get detail of JobOffer (as Operator)
        /// </summary>
        /// <param name="jobOfferId">JobOffer ID</param>
        /// <returns>JobOffer DTO</returns>
        public async Task<JobOfferWithJasRes> GetDetailOperatorAsync(int jobOfferId)
        {
            var jobOffer = await _context.JobOffers
                .Include(jo => jo.Address)
                .Include(jo => jo.Group)
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            // job offer not found
            if (jobOffer == null)
            {
                return null;
            }

            // get approved job application for job offer
            var jobApplications = await _context.JobApplications
                .Where(ja => (ja.JobOfferId == jobOffer.JobOfferId) && (ja.State != JobApplicationStates.Pending) && (ja.State != JobApplicationStates.Denied))
                .Include(ja => ja.Student)
                .ToListAsync();

            int freeSpaces = await GetFreeSpacesAsync(jobOffer.JobOfferId);
            return new JobOfferWithJasRes(jobOffer, jobApplications, freeSpaces);
        }

        /// <summary>
        /// Create JobOffer
        /// </summary>
        /// <param name="model">JobOffer DTO</param>
        /// <param name="userId">User ID</param>
        /// <returns>JobOffer DTO</returns>
        public async Task<JobOfferRes> CreateAsync(JobOfferReq model, int userId)
        {
            User user = await _context.Users
                .Include(us => us.Customer)
                    .ThenInclude(em => em.Group)
                .FirstOrDefaultAsync(us => us.UserId == userId);
            // create address
            Address address = _addressService.Create(model.Address);

            // create job offer
            JobOffer jobOffer = new JobOffer
            {
                Title = model.Title,
                Description = model.Description,
                Wage = model.Wage,
                Spaces = model.Spaces,
                Start = model.Start.ToUniversalTime(),
                End = model.End.ToUniversalTime(),
                Group = user.Customer.Group,
                Address = address
            };
            _context.JobOffers.Add(jobOffer);
            await _context.SaveChangesAsync();
            return new JobOfferRes(jobOffer);
        }

        /// <summary>
        /// Delete JobOffer
        /// </summary>
        /// <param name="jobOfferId">JobOffer ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>Bool if JobApplication was found</returns>
        public async Task<bool> DeleteAsync(int jobOfferId, int userId)
        {
            var user = await _context.Users
                .Include(us => us.Customer)
                .FirstOrDefaultAsync(us => us.UserId == userId);

            var jobOffer = await _context.JobOffers
                .Include(jo => jo.JobApplications)
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            // job offer not found
            if (jobOffer == null)
            {
                return false;
            }

            // check if job offer belongs to current group
            if (jobOffer.GroupId != user.Customer.GroupId)
            {
                throw new AppLogicException("Nabídka nepatří k dané skupině");
            }

            // check if job offer has already started
            if (jobOffer.Start.Date < DateTime.UtcNow.Date)
            {
                throw new AppLogicException("Nabídka již započala");
            }

            bool acceptedApplicationsExists = jobOffer.JobApplications.Any(ja => ja.State == JobApplicationStates.Approved);
            if (acceptedApplicationsExists)
            {
                throw new AppLogicException("Pro nabídku již existují potvrzené přihlášky");
            }


            // delete job offer
            _context.JobOffers.Remove(jobOffer);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Get count of JobOffer free spaces
        /// </summary>
        /// <param name="jobOfferId">JobOffer ID</param>
        /// <returns>Count of free spaces</returns>
        public async Task<int> GetFreeSpacesAsync(int jobOfferId)
        {
            var jobOffer = await _context.JobOffers
                .Include(jo => jo.JobApplications)
                .FirstOrDefaultAsync(jo => jo.JobOfferId == jobOfferId);

            int occupied = jobOffer.JobApplications.Count(ja => ja.State == JobApplicationStates.Approved);
            return jobOffer.Spaces - occupied;
        }
    }
}
