using Application.Core;
using Application.Domain.Entities;
using FresherManagement.Api.Infrastructures;
using FresherManagement.Api.Services;
using Infrastructure.Identity;
using Infrastructure.Identity.Context;
using Infrastructure.Identity.Settings;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace FresherManagement.Api
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
            services.AddSingleton(Configuration);

            services.AddScoped<IIdentityService, IdentityService>();
            //User Manager Service
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityDbContext(Configuration);
            services.AddPersistenceDbContext(Configuration);
            services.AddRepositories();
            services.AddApplicationServices();
            services.AddCustomApiVersioning();
            services.AddCustomSwagger();

            services.AddExternalServices();
            services.AddCustomCors(Configuration);

            // Configure enforce lowercase routing
            services.AddRouting(options => options.LowercaseUrls = true);

            // Add Newtonsoft.Json-based JSON format support and the default formatting is camelCase
            services.AddControllers().AddNewtonsoftJson().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseForwardedHeaders();
            app.UseDefaultFiles();
            //app.UseSerilogRequestLogging();
            app.UseCustomSwagger(provider);

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
