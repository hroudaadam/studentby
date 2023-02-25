using Studentby.App.Data.Database.Entities;

namespace Studentby.App.Data.Database.Repositories;

public interface IGroupRepository : IDatabaseRepository<Group>
{
    Task<bool> AnyWithNameAsync(string name);
    Task<IEnumerable<Group>> ListAsync();
    Task<Group> GetByIdAsync(Guid groupId);
}
