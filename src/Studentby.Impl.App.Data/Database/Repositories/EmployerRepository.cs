using Microsoft.EntityFrameworkCore;
using Studentby.App.Data.Cache;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Repositories;

namespace Studentby.Impl.App.Data.Database.Repositories;

internal class EmployerRepository : DatabaseRepository<Employer>, IEmployerRepository
{
    public EmployerRepository(SqlDbContext dbContext, ICacheService<Employer> cacheService, CacheStrategy cacheStrategy)
        : base(dbContext, cacheService, cacheStrategy)
    { }

    public async Task<Employer> GetByUserIdAsync(Guid userId)
    {
        return await ReadDataAsync(
            Specification()
            .AddInclude(q => q.Include(e => e.User).Include(e => e.Group))
            .SetFilter(e => e.UserId == userId)
            .Build(),
            q => q.SingleOrDefaultAsync());
    }

    public async Task<IEnumerable<Employer>> FiltredListAsync(Guid? groupId = null)
    {
        var specificationBuilder = Specification();
        if (groupId != null)
        {
            specificationBuilder.SetFilter(e => e.GroupId == groupId);
        }
        
        return await ReadDataAsync(
            specificationBuilder
            .AddInclude(q => q.Include(e => e.User))
            .Build(),
            q => q.ToListAsync());
    }
}