using Studentby.App.Data.Database.Entities;

namespace Studentby.App.Data.Database.Repositories;

public interface IUserRepository : IDatabaseRepository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<bool> AnyWithEmailAsync(string email);
}
