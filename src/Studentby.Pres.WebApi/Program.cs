using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Studentby.App.Logic;
using Studentby.Impl.App.Data;
using Studentby.Impl.Shared;
using Studentby.Pres.WebApi;
using Studentby.Pres.WebApi.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var logging = builder.Logging;
var services = builder.Services;
var env = builder.Environment;

// configuration providers
configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// logging setup
logging
    .ClearProviders()
    .AddConsole();

// services registration
services
    .AddShared(configuration)
    .AddApplicationData(configuration)
    .AddApplicationLogic(configuration)
    .AddWebApi(configuration);

// application pipeline
var app = builder.Build();
var logger = app.Services.GetService<ILogger<Program>>();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

app.UseHttpRequesLogging();

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions.Value);

app.UseErrorHandler();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint(
    "/swagger/v1/swagger.json",
    $"{Assembly.GetAssembly(typeof(Program)).FullName} v1"));

app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.UseSpa(c => c.Options.SourcePath = "wwwroot");

logger.LogInformation("Web application was successfully created");
app.Run();