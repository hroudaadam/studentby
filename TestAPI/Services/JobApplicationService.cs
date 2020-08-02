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
    public interface IJobApplicationService
    {
        Task<JobApplicationCreateResponse> CreateJobApplicationAsync(JobApplicationCreateRequest model, int userId);
        Task<IEnumerable<JobApplicationResponse>> GetStudentApplicationsAsync(int userId);
    }

    public class JobApplicationService: IJobApplicationService
    {
        private readonly StudentbyTestContext _context;

        public JobApplicationService(StudentbyTestContext context)
        {
            _context = context;
        }

        public async Task<JobApplicationCreateResponse> CreateJobApplicationAsync(JobApplicationCreateRequest model, int userId)
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

        public async Task<IEnumerable<JobApplicationResponse>> GetStudentApplicationsAsync(int userId)
        {
            var jobApplications = await _context.JobApplications
                .Include(ap => ap.Student)
                    .ThenInclude(st => st.User)
                .Include(ap => ap.JobOffer)
                    .ThenInclude(of => of.Company)
                .Where(ap => ap.Student.User.UserId == userId)
                .ToListAsync();

            List<JobApplicationResponse> result = new List<JobApplicationResponse>();

            foreach (var jobApplication in jobApplications)
            {
                result.Add(new JobApplicationResponse(jobApplication));
            }

            return result;
        }

    }
}
