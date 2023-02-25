using Microsoft.EntityFrameworkCore;
using Studentby.App.Data.Cache;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Repositories;

namespace Studentby.Impl.App.Data.Database.Repositories;

internal class GroupRepository : DatabaseRepository<Group>, IGroupRepository
{
    public GroupRepository(SqlDbContext dbContext, ICacheService<Group> cacheService, CacheStrategy cacheStrategy)
        : base(dbContext, cacheService, cacheStrategy)
    { }

    public async Task<bool> AnyWithNameAsync(string name)
    {
        return await ReadDataAsync(
            Specification().SetFilter(g => g.Name == name).Build(),
            q => q.AnyAsync()
        );
    }

    public async Task<IEnumerable<Group>> ListAsync()
    {
        return await ReadDataAsync(
            Specification()
            .Build(),
            q => q.ToListAsync());
    }

    public async Task<Group> GetByIdAsync(Guid groupId)
    {
        return await ReadDataAsync(
            Specification()
            .SetFilter(g => g.Id == groupId)
            .Build(),
            q => q.SingleOrDefaultAsync());
    }
}