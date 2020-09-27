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

    public class JobApplicationService: IJobApplicationService
    {
        private readonly StudentbyContext _context;
        private readonly IJobOfferService _jobOfferService;

        public JobApplicationService(StudentbyContext context, IJobOfferService jobOfferService)
        {
            _context = context;
            _jobOfferService = jobOfferService;
        }

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
        public async Task<IEnumerable<JobApplicationSimpleRes>> GetListOperatorAsync()
        {
            // pouze nezpracované
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

        public async Task<JobApplicationWithJoRes> GetDetailStudentAsync(int jobApplicationId, int userId)
        {
            var jobApplication = await _context.JobApplications
                .Include(ja => ja.JobOffer).ThenInclude(jo => jo.Group)
                .Include(ja => ja.JobOffer).ThenInclude(jo => jo.Address)
                .FirstOrDefaultAsync(ja => ja.JobApplicationId == jobApplicationId);
            
            // kontrola existence přihlášky
            if (jobApplication == null)
            {
                return null;
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(us => us.UserId == userId);

            // kontrola příslušnosti k uživateli
            if (jobApplication.StudentId != user.StudentId)
            {
                throw new StudentbyException("Přihláška napatří studentovi");
            }

            int freeSpaces = await _jobOfferService.GetFreeSpacesAsync(jobApplication.JobOfferId);
            return new JobApplicationWithJoRes(jobApplication, freeSpaces);
        }
        public async Task<JobApplicationWithJoAndStudRes> GetDetailOperatorAsync(int jobApplicationId)
        {
            var jobApplication = await _context.JobApplications
                .Include(ja => ja.Student).ThenInclude(st => st.User)
                .Include(ja => ja.Student).ThenInclude(st => st.Address)
                .Include(ja => ja.JobOffer).ThenInclude(jo => jo.Group)
                .Include(ja => ja.JobOffer).ThenInclude(jo => jo.Address)
                .FirstOrDefaultAsync(ja => ja.JobApplicationId == jobApplicationId);

            if (jobApplication == null)
            {
                return null;
            }
                
            int freeSpaces = await _jobOfferService.GetFreeSpacesAsync(jobApplication.JobOfferId);
            return new JobApplicationWithJoAndStudRes(jobApplication, freeSpaces);
        }
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

        public async Task<JobApplicationRes> CreateAsync(JobApplicationReq model, int userId)
        {
            // kontrola existence nabídky
            JobOffer jobOffer = await _context.JobOffers.FirstOrDefaultAsync(x => x.JobOfferId == model.JobOfferId);
            if (jobOffer == null)
            {
                throw new StudentbyException("Nabídka nenalezena");
            }

            // kontrola existence studenta
            Student student = await _context.Students
                .Include(student => student.User)
                .Include(st => st.JobApplications)
                    .ThenInclude(ja => ja.JobOffer)
                .Where(student => student.User.UserId == userId)
                .FirstOrDefaultAsync();
            if (student == null)
            {
                throw new StudentbyException("Uživatel nenalezen");
            }

            // kontrola data v budoucnosti
            // ...

            // kontrola duplicity přihlášky pro nabídku
            bool applicationExists = await _context.JobApplications.AnyAsync((ja) =>  
                ja.JobOfferId == jobOffer.JobOfferId &&
                ja.StudentId == student.StudentId);
            if (applicationExists)
            {
                throw new StudentbyException("Přihláška již existuje");
            }

            // kontrola duplicity přihlášky v časovém intervalu
            foreach (var ja in student.JobApplications)
            {
                if (ja.JobOffer.Start <= jobOffer.End && ja.JobOffer.End >= jobOffer.Start)
                {
                    throw new StudentbyException("Přihláška se překrývá s termínem jiné přihlášky");
                }
            }

            // vytvoření přihlášky
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
        public async Task<bool> DeleteAsync(int jobApplicationId)
        {
            var jobApplication = await _context.JobApplications
                .Include(ja => ja.JobOffer)
                .FirstOrDefaultAsync(ja => ja.JobApplicationId == jobApplicationId);

            // kontrola, zda přihláška existuje
            if (jobApplication == null)
            {
                return false;
            }

            // kontrola, zda je přihláška nezpracovaná
            if (jobApplication.State != JobApplicationStates.Pending)
            {
                throw new StudentbyException("Přihláška je již zpracovaná");
            }

            // kontrola, zda práce již začala
            bool hasJobStarted = jobApplication.JobOffer.Start.Date <=
                DateTime.Today.ToUniversalTime();
            if (hasJobStarted)
            {
                throw new StudentbyException("Práce již započala");
            }

            _context.JobApplications.Remove(jobApplication);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditAsync(int jobApplicationId, JobApplicationDetailReq model)
        {
            if (jobApplicationId != model.JobApplicationId)
            {
                throw new StudentbyException("Neplatný požadavek");
            }

            var jobApplication = await _context.JobApplications.FirstAsync(ja => ja.JobApplicationId == jobApplicationId);
            if (jobApplication == null)
            {
                return false;
            }

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

            jobApplication.State = model.State; 
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
