using Application.Core.Interfaces.Services;
using Infrastructure.Shared.Email;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
