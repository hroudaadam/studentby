using Studentby.App.Data.Database.Entities;

namespace Studentby.App.Data.Database.Repositories;

public interface IEmployerRepository : IDatabaseRepository<Employer>
{
    Task<Employer> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Employer>> FiltredListAsync(Guid? groupId = null);
}
