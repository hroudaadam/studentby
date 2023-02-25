using Microsoft.EntityFrameworkCore;
using Studentby.App.Data.Cache;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Repositories;
using Studentby.Shared.Structures;

namespace Studentby.Impl.App.Data.Database.Repositories;

internal class JobOfferRepository : DatabaseRepository<JobOffer>, IJobOfferRepository
{
    public JobOfferRepository(SqlDbContext dbContext, ICacheService<JobOffer> cacheService, CacheStrategy cacheStrategy)
        : base(dbContext, cacheService, cacheStrategy)
    { }

    public async Task<JobOffer> GetByIdAsync(Guid jobOfferId)
    {
        return await ReadDataAsync(
            Specification()
            .AddInclude(q => q
                .Include(jo => jo.JobApplications)
                .Include(jo => jo.Group).
                Include(jo => jo.Address))
            .SetFilter(jo => jo.Id == jobOfferId)
            .Build(),
            q => q.SingleOrDefaultAsync());
    }

    public async Task<PaginatedList<JobOffer>> FiltredListAsync(
        int page, 
        int pageSize, 
        Guid? groupId = null,
        string city = null,
        DateTime? date = null)
    {
        var specificationBuilder = Specification();
        if (groupId != null) 
        {
            specificationBuilder.SetFilter(jo => jo.GroupId == groupId);
        }
        if (city != null)
        {
            specificationBuilder.SetFilter(jo => jo.Address.City == city);
        }
        if (date != null)
        {
            specificationBuilder.SetFilter(jo => jo.Start.Date == date.Value.Date);
        }

        return await ReadPaginatedDataAsync(
            specificationBuilder
            .AddInclude(q => q
                .Include(jo => jo.Address)
                .Include(jo => jo.Group))
            .Build(),
            pageSize,
            page);
    }
}