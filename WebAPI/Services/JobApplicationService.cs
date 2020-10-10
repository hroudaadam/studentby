using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IJobApplicationService
    {
        Task<JobApplicationRes> CreateAsync(JobApplicationReq model, int userId);
        Task<IEnumerable<JobApplicationSimpleRes>> GetListStudentAsync(int userId);
        Task<JobApplicationWithJoRes> GetDetailStudentAsync(int jobApplicationId, int userId);
        Task<bool> DeleteAsync(int jobApplicationId);
        Task<IEnumerable<JobApplicationSimpleRes>> GetListOperatorAsync();
        Task<JobApplicationWithJoAndStudRes> GetDetailOperatorAsync(int jobApplicationId);
        Task<bool> EditAsync(int jobApplicationId, JobApplicationDetailReq model);
        Task<JobApplicationWithStudRes> GetResultAsync(int jobApplicationId);
    }

    /// <summary>
    /// Service for JobApplication operations
    /// </summary>
    public class JobApplicationService: IJobApplicationService
    {
        private readonly StudentbyContext _context;
        private readonly IJobOfferService _jobOfferService;

        public JobApplicationService(StudentbyContext context, IJobOfferService jobOfferService)
        {
            _context = context;
            _jobOfferService = jobOfferService;
        }

        /// <summary>
        /// Get list of JobApplications (as Student)
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>List of JobApplication DTOs</returns>
        public async Task<IEnumerable<JobApplicationSimpleRes>> GetListStudentAsync(int userId)
        {
            var jobApplications = await _context.JobApplications
                .Include(ap => ap.Student)
                    .ThenInclude(st => st.User)
                .Include(ap => ap.JobOffer)
                .Where(ap => ap.Student.User.UserId == userId)
                .ToListAsync();

            List<JobApplicationSimpleRes> result = new List<JobApplicationSimpleRes>();
            foreach (var jobApplication in jobApplications)
            {
                result.Add(new JobApplicationSimpleRes(jobApplication));
            }
            return result;
        }

        /// <summary>
        /// Get list of JobApplications (as Operator)
        /// </summary>
        /// <returns>List of JobApplication DTOs</returns>
        public async Task<IEnumerable<JobApplicationSimpleRes>> GetListOperatorAsync()
        {
            var jobApplications = await _context.JobApplications
                                      .Where(ja => ja.State == JobApplicationStates.Pending)   
                                      .Include(ja => ja.JobOffer)
                                      .ToListAsync();

            List<JobApplicationSimpleRes> result = new List<JobApplicationSimpleRes>();
            foreach (var jobApplication in jobApplications)
            {
                result.Add(new JobApplicationSimpleRes(jobApplication));
            }
            return result;
        }

        /// <summary>
        /// Get detail of JobApplication (as Student)
        /// </summary>
        /// <param name="jobApplicationId">JobApplication ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>JobApplication DTO</returns>
        public async Task<JobApplicationWithJoRes> GetDetailStudentAsync(int jobApplicationId, int userId)
        {
            var jobApplication = await _context.JobApplications
                .Include(ja => ja.JobOffer).ThenInclude(jo => jo.Group)
                .Include(ja => ja.JobOffer).ThenInclude(jo => jo.Address)
                .FirstOrDefaultAsync(ja => ja.JobApplicationId == jobApplicationId);
            
            // jobApplication not found
            if (jobApplication == null)
            {
                return null;
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(us => us.UserId == userId);

            // jobApplication does not belong to student
            if (jobApplication.StudentId != user.StudentId)
            {
                throw new StudentbyException("Přihláška napatří studentovi");
            }

            int freeSpaces = await _jobOfferService.GetFreeSpacesAsync(jobApplication.JobOfferId);
            return new JobApplicationWithJoRes(jobApplication, freeSpaces);
        }

        /// <summary>
        /// Get detail of JobApplication (as Operator)
        /// </summary>
        /// <param name="jobApplicationId">JobApplication ID</param>
        /// <returns>JobApplication DTO</returns>
        public async Task<JobApplicationWithJoAndStudRes> GetDetailOperatorAsync(int jobApplicationId)
        {
            var jobApplication = await _context.JobApplications
                .Include(ja => ja.Student).ThenInclude(st => st.User)
                .Include(ja => ja.Student).ThenInclude(st => st.Address)
                .Include(ja => ja.JobOffer).ThenInclude(jo => jo.Group)
                .Include(ja => ja.JobOffer).ThenInclude(jo => jo.Address)
                .FirstOrDefaultAsync(ja => ja.JobApplicationId == jobApplicationId);

            // jobApplication not found - 404
            if (jobApplication == null)
            {
                return null;
            }
                
            int freeSpaces = await _jobOfferService.GetFreeSpacesAsync(jobApplication.JobOfferId);
            return new JobApplicationWithJoAndStudRes(jobApplication, freeSpaces);
        }

        // !!! duplicty with GetDetailOperator (another endpoint just to reduce JSON size)
        /// <summary>
        /// Get detail of JobApplication (as Operator)
        /// </summary>
        /// <param name="jobApplicationId">JobApplication ID</param>
        /// <returns>JobApplication DTO</returns>
        public async Task<JobApplicationWithStudRes> GetResultAsync(int jobApplicationId)
        {
            var jobApplication = await _context.JobApplications
                .Include(ja => ja.Student).ThenInclude(st => st.User)
                .Include(ja => ja.Student).ThenInclude(st => st.Address)
                .FirstOrDefaultAsync(ja => ja.JobApplicationId == jobApplicationId);

            if (jobApplication == null)
            {
                return null;
            }
            return new JobApplicationWithStudRes(jobApplication);
        }

        /// <summary>
        /// Create JobApplication
        /// </summary>
        /// <param name="model">JobApplication DTO</param>
        /// <param name="userId">User ID</param>
        /// <returns>JobApplication DTO</returns>
        public async Task<JobApplicationRes> CreateAsync(JobApplicationReq model, int userId)
        {
            JobOffer jobOffer = await _context.JobOffers.FirstOrDefaultAsync(x => x.JobOfferId == model.JobOfferId);
            // jobOffer not found
            if (jobOffer == null)
            {
                throw new StudentbyException("Nabídka nenalezena");
            }

            Student student = await _context.Students
                .Include(student => student.User)
                .Where(student => student.User.UserId == userId)
                .Include(st => st.JobApplications)
                    .ThenInclude(ja => ja.JobOffer)
                .FirstOrDefaultAsync();

            // check if job offer start date is in future
            // ...

            // check if job application for this job offer already exists
            bool applicationExists = await _context.JobApplications.AnyAsync((ja) =>  
                ja.JobOfferId == jobOffer.JobOfferId &&
                ja.StudentId == student.StudentId);
            if (applicationExists)
            {
                throw new StudentbyException("Přihláška pro tuto nabídku již existuje");
            }

            // check if job application in the same time interval already exists
            foreach (var ja in student.JobApplications)
            {
                if (ja.JobOffer.Start <= jobOffer.End && ja.JobOffer.End >= jobOffer.Start)
                {
                    throw new StudentbyException("Přihláška se překrývá s termínem jiné přihlášky");
                }
            }

            // create job application
            JobApplication jobApplication = new JobApplication
            {
                State = JobApplicationStates.Pending,
                Student = student,
                JobOffer = jobOffer
            };
            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();

            return new JobApplicationRes(jobApplication);
        }
        
        /// <summary>
        /// Delete JobApplication
        /// </summary>
        /// <param name="jobApplicationId">JobApplication ID</param>
        /// <returns>Bool if JobApplication was found</returns>
        public async Task<bool> DeleteAsync(int jobApplicationId)
        {
            var jobApplication = await _context.JobApplications
                .Include(ja => ja.JobOffer)
                .FirstOrDefaultAsync(ja => ja.JobApplicationId == jobApplicationId);

            // job application not found
            if (jobApplication == null)
            {
                return false;
            }

            // check if job application state is not pending
            if (jobApplication.State != JobApplicationStates.Pending)
            {
                throw new StudentbyException("Přihláška je již zpracovaná");
            }

            // delete job application
            _context.JobApplications.Remove(jobApplication);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Edit JobApplication
        /// </summary>
        /// <param name="jobApplicationId">JobApplication ID</param>
        /// <param name="model">JobApplication DTO</param>
        /// <returns>Bool if JobApplication was found</returns>
        public async Task<bool> EditAsync(int jobApplicationId, JobApplicationDetailReq model)
        {
            // route id and model id differs
            if (jobApplicationId != model.JobApplicationId)
            {
                throw new StudentbyException("Neplatný požadavek");
            }

            var jobApplication = await _context.JobApplications.FirstAsync(ja => ja.JobApplicationId == jobApplicationId);
            // job application not found
            if (jobApplication == null)
            {
                return false;
            }

            // check if the transition between states is valid
            bool toPenErr = model.State == JobApplicationStates.Pending;
            bool toAppErr = (model.State == JobApplicationStates.Approved) &&
                (jobApplication.State != JobApplicationStates.Pending);
            bool toDenErr = (model.State == JobApplicationStates.Denied) &&
                (jobApplication.State != JobApplicationStates.Pending);
            bool toAbsErr = (model.State == JobApplicationStates.Absent) &&
                ((jobApplication.State != JobApplicationStates.Attended) &&
                (jobApplication.State != JobApplicationStates.Approved));
            bool toAttErr = (model.State == JobApplicationStates.Attended) &&
                ((jobApplication.State != JobApplicationStates.Absent) &&
                (jobApplication.State != JobApplicationStates.Approved));

            if (toPenErr || toAppErr || toDenErr || toAbsErr || toAttErr)
            {
                throw new StudentbyException("Nevalidní přechod mezi stavy přihlášky");
            }

            // edit job application
            jobApplication.State = model.State; 
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
