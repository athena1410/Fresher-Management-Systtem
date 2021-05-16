using Application.Core.DTOs.Emails;
using Application.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Emails
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendAsync(EmailMessageDto emailMessage)
        {
            // TODO: Implement with real email provider
            return await Task.FromResult(true);
        }
    }
}
