using System.Reflection;
using VehicleTracking.Application.Infrastructure;
using VehicleTracking.Common;
using VehicleTracking.Infrastructure;
using VehicleTracking.Persistence;
using MediatR;
using MediatR.Pipeline;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using VehicleTracking.WebApi.Middlewares;
using FluentValidation;
using VehicleTracking.Persistence.Repositories;
using VehicleTracking.Persistence.Interfaces;
using VehicleTracking.Application.VehicleModule.Commands;
using VehicleTracking.Application.VehicleModule.Queries;
using VehicleTracking.Application.VehicleModule.Validations;
using VehicleTracking.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using VehicleTracking.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VehicleTracking.Security.Interfaces;
using VehicleTracking.Security;
using VehicleTracking.Security.Validations;
using VehicleTracking.Security.Models;
using System.Threading.Tasks;

namespace VehicleTracking.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
              .AddFluentValidation();

            services.AddDbContext<VehicleTrackingDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("VehicleDb")));

            services.AddDbContext<TrackingDbContext>(options =>
                       options.UseSqlServer(Configuration.GetConnectionString("TrackingPointDb")));

            services
                .AddIdentity<UsersSystem, IdentityRole<Guid>>(options => {
                    //testing purpose
                    options.Password.RequireDigit = false; 
                    options.Password.RequiredLength = 4;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<VehicleTrackingDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton(Configuration);

            //Repository
            services.AddScoped(typeof(ITrackingRepository<>), typeof(TrackingRepository<>));
            services.AddScoped(typeof(IVehicleTrackingRepository<>), typeof(VehicleTrackingRepository<>));

            //MediatR Pipeline
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddSingleton(typeof(IRequestPreProcessor<>), typeof(RequestLogger<>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddMediatR(typeof(RegisterVehicleCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RecordPositionCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CurrentPositionQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(JourneyQueryHandler).GetTypeInfo().Assembly);

            //Infrastructure
            services.AddSingleton<IDateTime, MachineDateTime>();

            //validations
            services.AddSingleton<IValidator<CurrentPositionQuery>, CurrentPositionQueryValidation>();
            services.AddSingleton<IValidator<JourneyQuery>, JourneyQueryValidation>();
            services.AddSingleton<IValidator<RecordPositionCommand>, RecordPositionCommandValidation>();
            services.AddSingleton<IValidator<RegisterVehicleCommand>, RegisterVehicleCommandValidation>();
            services.AddSingleton<IValidator<LoginModel>, LoginValidation>();
            services.AddSingleton<IValidator<RegisterModel>, RegisterValidation>();

            //Geo service
            services.AddSingleton<IGeocodingService, GeocodingService>();

            //JWT
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            services
                .AddAuthorization(options=> {
                    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Configurations:JwtIssuer"],
                        ValidAudience = Configuration["Configurations:JwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Configurations:JwtKey"])),
                        ClockSkew = TimeSpan.Zero, // remove delay of token when expire,
                       
                    };
                });

            services.AddTransient<IUserService, UserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IHostingEnvironment env
            , ILoggerFactory loggerFactory
            , IServiceProvider serviceProvider
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            env.ConfigureNLog("nlog.config");

            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            //add NLog.Web
#pragma warning disable CS0618 // Type or member is obsolete
            app.AddNLogWeb();
#pragma warning restore CS0618 // Type or member is obsolete

            app.UseStaticFiles();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMvc();

            //create default admin for testing purpose
            var userService = serviceProvider.GetRequiredService<IUserService>();
            userService.CreateRolesAsync().Wait();
            //
        }
    }
}
