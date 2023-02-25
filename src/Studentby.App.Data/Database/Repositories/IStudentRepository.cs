using Studentby.App.Data.Database.Entities;

namespace Studentby.App.Data.Database.Repositories;

public interface IStudentRepository : IDatabaseRepository<Student>
{
    Task<Student> GetByUserIdAsync(Guid userId);
    Task<Student> GetByIdAsync(Guid studentId);
    Task<IEnumerable<Student>> ListAsync();
}
