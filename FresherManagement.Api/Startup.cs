using Application.Core;
using Application.Core.Constants;
using Application.Domain.Entities;
using FresherManagement.Api.Infrastructures;
using FresherManagement.Api.Services;
using FresherManagement.Api.SignalR;
using Infrastructure.Identity;
using Infrastructure.Identity.Context;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.Json;
using FresherManagement.Api.Infrastructures.Mappings;

namespace FresherManagement.Api
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddSingleton(Configuration);
            services.AddScoped<IIdentityService, IdentityService>();
            //User Manager Service
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityDbContext(Configuration);
            services.AddJwtAuthentication(Configuration);
            services.AddAuthorization();
            services.AddPersistenceDbContext(Configuration);
            services.AddRepositories();
            services.AddApplicationServices();
            services.AddCustomApiVersioning();
            services.AddCustomSwagger();
            services.AddExternalServices();
            services.AddCustomCors(Configuration);
            services.AddSignalR();
            services.AddConfigureFormOption();
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

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
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), Constants.FolderPath.ASSETS)),
                RequestPath = new PathString($"/{Constants.FolderPath.ASSETS}")
            });
            app.UseSerilogRequestLogging();
            app.UseCustomSwagger(provider);

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(AppSettings.CORS_POLICY);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AccountEventsClientHub>("/account-events");
            });
        }
    }
}
