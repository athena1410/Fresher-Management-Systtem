using Application.Core.DTOs.Emails;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendAsync(EmailMessageDto emailMessage);
    }
}
