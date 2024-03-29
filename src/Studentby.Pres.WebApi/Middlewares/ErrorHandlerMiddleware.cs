﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Studentby.App.Data.Localization;
using Studentby.Pres.WebApi.Models.Common;
using Studentby.Shared.Exceptions;

namespace Studentby.Pres.WebApi.Middlewares;

internal class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IStringLocalizer<Strings> localizer, ILogger<ErrorHandlerMiddleware> logger)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var error = HandleException(ex, localizer);
            if (error.StatusCode == 500)
            {
                logger.LogError("{ex}", ex);
            }
            httpContext.Response.StatusCode = error.StatusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(error);
        }
    }

    public ErrorRes HandleException(Exception ex, IStringLocalizer<Strings> localizer)
    {
        string exceptionName = ex.GetType().Name;

        return ex switch
        {
            NotFoundException notFoundException => new ErrorRes(404, localizer[notFoundException.MessageKey]),
            BadLogicException badLogicException => new ErrorRes(400, localizer[badLogicException.MessageKey]),
            ValidationException validationException => new ErrorRes(400, localizer[validationException.MessageKey], validationException.Details),
            NotAuthenticatedException notAuthenticatedException => new ErrorRes(401, localizer[notAuthenticatedException.MessageKey]),
            NotAuthorizedException notAuthorizedException => new ErrorRes(403, localizer[notAuthorizedException.MessageKey]),
            _ => new ErrorRes(500, localizer["ServerErrorMessage"]),
        };
    }
}
