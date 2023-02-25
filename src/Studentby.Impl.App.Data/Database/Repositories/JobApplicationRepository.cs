using Microsoft.EntityFrameworkCore;
using Studentby.App.Data.Cache;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;

namespace Studentby.Impl.App.Data.Database.Repositories;

internal class JobApplicationRepository : DatabaseRepository<JobApplication>, IJobApplicationRepository
{
    public JobApplicationRepository(SqlDbContext dbContext, ICacheService<JobApplication> cacheService, CacheStrategy cacheStrategy)
        : base(dbContext, cacheService, cacheStrategy)
    { }

    public async Task<JobApplication> GetByIdAsync(Guid jobApplicationId)
    {
        return await ReadDataAsync(
            Specification()
            .SetFilter(ja => ja.Id == jobApplicationId)
            .AddInclude(q => q
                .Include(ja => ja.JobOffer)
                    .ThenInclude(jo => jo.Address)
                .Include(ja => ja.JobOffer)
                    .ThenInclude(jo => jo.Group)
                .Include(ja => ja.Student)
                    .ThenInclude(st => st.User)
                .Include(ja => ja.Student)
                    .ThenInclude(st => st.Address))
            .Build(),
            q => q.SingleOrDefaultAsync());
    }

    public async Task<IEnumerable<JobApplication>> ListAliveByStudentIdAsync(Guid studentId)
    {
        return await ReadDataAsync(
            Specification()
            .SetFilter(ja => ja.StudentId == studentId && 
                (ja.State == JobApplicationState.Pending || ja.State == JobApplicationState.Approved))
            .Build(),
            q => q.ToListAsync());
    }

    public async Task<IEnumerable<JobApplication>> FiltredListAsync(Guid? studentId = null, Guid? jobOfferId = null)
    {
        var specificationBuilder = Specification();
        if (studentId != null) specificationBuilder.SetFilter(ja => ja.StudentId == studentId);
        if (jobOfferId != null) specificationBuilder.SetFilter(ja => ja.JobOfferId == jobOfferId);

        return await ReadDataAsync(
            specificationBuilder
            .AddInclude(q => q
                .Include(ja => ja.JobOffer)
                    .ThenInclude(jo => jo.Address)
                .Include(ja => ja.JobOffer)
                    .ThenInclude(jo => jo.Group)
                .Include(ja => ja.Student)
                    .ThenInclude(st => st.User)
                .Include(ja => ja.Student)
                    .ThenInclude(st => st.Address))
            .Build(),
            q => q.ToListAsync());
    }

}