using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Studentby.App.Logic.Common;
using Studentby.App.Logic.Common.Mediator.Behaviors;
using System.Reflection;

namespace Studentby.App.Logic;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLogic(this IServiceCollection services, IConfiguration configuration)
    {
        var mapperConfig = TypeAdapterConfig.GlobalSettings;
        mapperConfig.Scan(Assembly.GetExecutingAssembly());

        services.Configure<ApplicationSettings>(configuration.GetSection(ApplicationSettings.CONFIG_KEY));

        ValidatorOptions.Global.LanguageManager.Enabled = false;
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}