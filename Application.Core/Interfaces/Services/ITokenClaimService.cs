using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Application.Domain.Entities;

namespace Application.Core.Interfaces.Services
{
    public interface ITokenClaimService
    {
        Task<JwtSecurityToken> GenerateTokenAsync(ApplicationUser user);
        RefreshToken GenerateRefreshToken();
    }
}
