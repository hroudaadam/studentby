using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Studentby.App.Data.Cache;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Repositories;
using Studentby.Impl.App.Data.Cache;
using Studentby.Impl.App.Data.Database;
using Studentby.Impl.App.Data.Database.Repositories;

namespace Studentby.Impl.App.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationData(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheSettings = configuration.GetSection(CacheSettings.CONFIG_KEY).Get<CacheSettings>();
        services
            .Configure<CacheSettings>(configuration.GetSection(CacheSettings.CONFIG_KEY))
            .AddMemoryCache(o => o.SizeLimit = cacheSettings.Size);

        services.AddDbContext<SqlDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Sql")));

        services
            .AddDatabaseRepository<JobOffer, IJobOfferRepository, JobOfferRepository>()
            .AddDatabaseRepository<JobApplication, IJobApplicationRepository, JobApplicationRepository>()
            .AddDatabaseRepository<Group, IGroupRepository, GroupRepository>()
            .AddDatabaseRepository<Employer, IEmployerRepository, EmployerRepository>()
            .AddDatabaseRepository<Student, IStudentRepository, StudentRepository>()
            .AddDatabaseRepository<RefreshToken, IRefreshTokenRepository, RefreshTokenRepository>()
            .AddDatabaseRepository<User, IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddDatabaseRepository<TEntity, TService, TImplementation>(this IServiceCollection services)
        where TEntity : BaseEntity
        where TImplementation : TService
        where TService : ICachable
    {
        services.AddSingleton<ICacheService<TEntity>, CacheService<TEntity>>();

        // add not-cached repository
        services.AddScoped(typeof(TService), sp =>
        {
            return Activator.CreateInstance(
                typeof(TImplementation),
                sp.GetRequiredService<SqlDbContext>(),
                sp.GetRequiredService<ICacheService<TEntity>>(),
                CacheStrategy.Bypass);
        });

        // add cached repository
        services.AddScoped(typeof(Cached<TService>), sp =>
        {
            var repository = (TService)Activator.CreateInstance(
                typeof(TImplementation),
                sp.GetRequiredService<SqlDbContext>(),
                sp.GetRequiredService<ICacheService<TEntity>>(),
                CacheStrategy.Use);
            return new Cached<TService>(repository);
        });

        return services;
    }
}