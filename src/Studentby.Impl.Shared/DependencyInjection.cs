using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Studentby.Impl.Shared.Clock;
using Studentby.Impl.Shared.Security.Crypto;
using Studentby.Impl.Shared.Security.WebToken;
using Studentby.Shared.Clock;
using Studentby.Shared.Security;

namespace Studentby.Impl.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClockProvider, ClockProvider>();

        services
            .Configure<WebTokenSettings>(configuration.GetSection(WebTokenSettings.CONFIG_KEY))
            .AddTransient<IWebTokenService, WebTokenService>();

        services.AddTransient<ICryptographyService, CryptographyService>();

        return services;
    }
}