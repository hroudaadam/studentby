using Studentby.App.Data.Database.Entities;

namespace Studentby.App.Data.Database.Repositories;

public interface IJobApplicationRepository : IDatabaseRepository<JobApplication>
{
    Task<JobApplication> GetByIdAsync(Guid jobApplicationId);
    Task<IEnumerable<JobApplication>> ListAliveByStudentIdAsync(Guid studentId);
    Task<IEnumerable<JobApplication>> FiltredListAsync(Guid? studentId = null, Guid? jobOfferId = null);
}
