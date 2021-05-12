using Application.Core.DTOs.Email;
using Application.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Email
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendAsync(EmailMessage emailMessage)
        {
            // TODO: Implement with real email provider
            return await Task.FromResult(true);
        }
    }
}
