using System;
using System.Text;
using Application.Core.Interfaces;
using Application.Core.Interfaces.Services;
using Infrastructure.Identity.Context;
using Infrastructure.Identity.DataSeed;
using Infrastructure.Identity.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Application.Core.Constants;
using Infrastructure.Identity.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(AppSettings.IDENTITY_CONNECTION_STRING),
                    b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.GetName().Name));
            });

            services.AddScoped(typeof(DbContext), typeof(IdentityContext));
            services.AddTransient<ITokenClaimService, TokenClaimService>();
            services.AddScoped<IDataSeeder, DataSeeder>();
            var dataSeeder = services.BuildServiceProvider().GetService<IDataSeeder>();
            if (dataSeeder != null)
            {
                Task.Run(async () => await dataSeeder.SeedAsync());
            }
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            IConfigurationSection jwtConfigurationSection = configuration.GetSection(AppSettings.JWT_SETTINGS_SECTION);
            services.Configure<JWTSettings>(jwtConfigurationSection);
            var jwtSettings = jwtConfigurationSection.Get<JWTSettings>();
            //Adding Authentication - JWT
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                });

            return services;
        }
    }
}
