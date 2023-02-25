using Microsoft.EntityFrameworkCore;
using Studentby.App.Data.Cache;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Repositories;
using Studentby.Shared.Helpers;

namespace Studentby.Impl.App.Data.Database.Repositories;

internal class RefreshTokenRepository : DatabaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(SqlDbContext dbContext, ICacheService<RefreshToken> cacheService, CacheStrategy cacheStrategy)
        : base(dbContext, cacheService, cacheStrategy)
    { }

    public async Task<RefreshToken> GetByTokenAsync(string refreshToken)
    {
        return await ReadDataAsync(
            Specification()
            .SetFilter(rt => rt.Token == refreshToken)
            .AddInclude(q => q.Include(rt => rt.User))
            .Build(),
            q => q.SingleOrDefaultAsync());
    }

    public async Task<RefreshToken> GetByTokenAndUserIdAsync(string refreshToken, Guid userId)
    {
        return await ReadDataAsync(
            Specification()
            .SetFilter(rt => rt.Token == refreshToken && rt.UserId == userId)
            .Build(),
            q => q.SingleOrDefaultAsync());
    }
}