using Studentby.App.Data.Cache;
using Studentby.App.Data.Database.Entities;

namespace Studentby.App.Data.Database.Repositories;

public interface IDatabaseRepository<T> : ICachable where T : BaseEntity
{
    Task<T> CreateAsync(T entity, bool commitAfter = true);
    Task<T> DeleteAsync(T entity, bool commitAfter = true);
    Task<T> UpdateAsync(T entity, bool commitAfter = true);
}
