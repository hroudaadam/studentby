using Studentby.App.Data.Database.Entities;

namespace Studentby.App.Data.Database.Repositories;

public interface IRefreshTokenRepository : IDatabaseRepository<RefreshToken>
{
    Task<RefreshToken> GetByTokenAsync(string refreshToken);
    Task<RefreshToken> GetByTokenAndUserIdAsync(string refreshToken, Guid userId);
}
