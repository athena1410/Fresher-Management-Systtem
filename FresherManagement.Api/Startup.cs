using Application.Core;
using Application.Core.Constants;
using Application.Domain.Entities;
using FresherManagement.Api.Infrastructures;
using FresherManagement.Api.Infrastructures.Mappings;
using FresherManagement.Api.Services;
using FresherManagement.Api.SignalR;
using Infrastructure.Identity;
using Infrastructure.Identity.Context;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

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
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSignalR();
            services.AddConfigureFormOption();

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

            // Fix JwtRegisteredClaimNames.Sub not mapping to 'sub'
            // https://github.com/IdentityServer/IdentityServer4/issues/2968
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

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
