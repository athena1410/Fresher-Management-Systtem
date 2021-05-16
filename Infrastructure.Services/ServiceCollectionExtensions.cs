using Application.Core.Interfaces.Services;
using Infrastructure.Shared.Emails;
using Infrastructure.Shared.Files;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IFileService, FileService>();

            return services;
        }
    }
}
