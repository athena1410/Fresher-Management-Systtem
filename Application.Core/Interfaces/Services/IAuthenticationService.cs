using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterAsync(string userName, string email, string password);
        Task<bool> ConfirmEmailAsync(string userName, string code);
    }
}
