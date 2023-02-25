using Studentby.App.Data.Database.Entities;
using Studentby.Shared.Structures;

namespace Studentby.App.Data.Database.Repositories;

public interface IJobOfferRepository : IDatabaseRepository<JobOffer>
{
    Task<JobOffer> GetByIdAsync(Guid jobOfferId);
    Task<PaginatedList<JobOffer>> FiltredListAsync(
        int page,
        int pageSize,
        Guid? groupId = null,
        string city = null,
        DateTime? date = null);
}
