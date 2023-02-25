using Microsoft.EntityFrameworkCore;
using Studentby.App.Data.Cache;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Repositories;

namespace Studentby.Impl.App.Data.Database.Repositories;

internal class StudentRepository : DatabaseRepository<Student>, IStudentRepository
{
    public StudentRepository(SqlDbContext dbContext, ICacheService<Student> cacheService, CacheStrategy cacheStrategy)
        : base(dbContext, cacheService, cacheStrategy)
    { }

    public async Task<Student> GetByIdAsync(Guid studentId)
    {
        return await ReadDataAsync(
            Specification()
            .AddInclude(q => q
                .Include(s => s.User)
                .Include(s => s.Address))
            .SetFilter(st => st.Id == studentId)
            .Build(),
            q => q.SingleOrDefaultAsync());
    }

    public async Task<Student> GetByUserIdAsync(Guid userId)
    {
        return await ReadDataAsync(
            Specification()
            .AddInclude(q => q
                .Include(s => s.User)
                .Include(s => s.Address))
            .SetFilter(st => st.UserId == userId)
            .Build(),
            q => q.SingleOrDefaultAsync());
    }

    public async Task<IEnumerable<Student>> ListAsync()
    {
        return await ReadDataAsync(
            Specification()
            .AddInclude(q => q
                .Include(s => s.Address)
                .Include(s => s.User))
            .Build(),
            q => q.ToListAsync());
    }
}