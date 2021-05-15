using Application.Core.DTOs.Email;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendAsync(EmailMessageDto emailMessage);
    }
}
