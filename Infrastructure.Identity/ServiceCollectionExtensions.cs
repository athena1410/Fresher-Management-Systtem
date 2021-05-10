using Infrastructure.Identity.Context;
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
            return services;
        }
    }
}
