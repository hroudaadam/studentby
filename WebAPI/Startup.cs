using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Services;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            // CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(
                            "http://localhost:8080",
                            "http://192.168.0.103:8080",
                            "http://192.168.0.104:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });


            // DataBase
            services.AddDbContext<StudentbyContext>(options =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // controllers
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    // serialization to UTC date format
                    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    // DTO validation errors
                    options.InvalidModelStateResponseFactory = cont =>
                    {
                        var errors = string.Join(", ", cont.ModelState.Values.Where(v => v.Errors.Count > 0)
                          .SelectMany(v => v.Errors)
                          .Select(v => v.ErrorMessage));

                        // global error handler
                        throw new StudentbyException(errors);
                    };
                });

            // AppSettings
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // JWT
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJobOfferService, JobOfferService>();
            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IAddressService, AddressService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
