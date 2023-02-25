using Microsoft.AspNetCore.Builder;
using Studentby.Pres.WebApi.Middlewares;

namespace Studentby.Pres.WebApi.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseHttpRequesLogging(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<HttpLoggingMiddleware>();
    }

    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}