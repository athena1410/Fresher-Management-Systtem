using Application.Core.Interfaces;
using Application.Core.Interfaces.Repositories;
using Application.Domain.Entities;
using Ardalis.Specification;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("Application"),
                    b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.GetName().Name));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped(typeof(IUnitOfWork), typeof(ApplicationContext));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            return services;
        }
    }
}
