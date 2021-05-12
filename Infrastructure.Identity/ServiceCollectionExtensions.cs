using Application.Core.Interfaces.Services;
using Infrastructure.Identity.Context;
using Infrastructure.Identity.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("Identity"),
                    b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.GetName().Name));
            });

            services.AddScoped(typeof(DbContext), typeof(IdentityContext));
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
