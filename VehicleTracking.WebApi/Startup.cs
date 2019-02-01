using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Application.Infrastructure;
using VehicleTracking.Application.Interfaces;
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
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using VehicleTracking.WebApi.Middlewares;
using VehicleTracking.Application.BankAccounts.Commands;
using Microsoft.AspNetCore.Mvc;
using VehicleTracking.Application.BankAccounts.Validations;
using FluentValidation;

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
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //MediatR Pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestLogger<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //services.AddMediatR(typeof(InquiryByAccountNumberQueryHandler).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(DepositCommandHandler).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(WithdrawCommandHandler).GetTypeInfo().Assembly);

            //Infrastructure
            services.AddTransient<IDateTime, MachineDateTime>();

            //validations
          


            //services.AddTransient<IValidator<DepositCommand>, DepositCommandValidation>();
            //services.AddTransient<IValidator<WithdrawCommand>, WithdrawCommandValidation>();
            //services.AddTransient<IValidator<InquiryByAccountNumberQuery>, InquiryByAccountNumberValidations>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IHostingEnvironment env
            , ILoggerFactory loggerFactory
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


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
        }
    }
}
