using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common.Guard;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Application.Core.Interfaces.Services;
using Application.Domain.Entities;
using Infrastructure.Identity.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Services
{
    public class TokenClaimService : ITokenClaimService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTSettings _jwtSettings;

        public TokenClaimService(
            UserManager<ApplicationUser> userManager,
            IOptions<JWTSettings> jwtOptions)
        {
            _userManager = Guard.NotNull(userManager, nameof(userManager));
            _jwtSettings = Guard.NotNull(jwtOptions.Value, nameof(jwtOptions));
        }

        public async Task<JwtSecurityToken> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(x => new Claim("roles", x));
            var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id)
                }
                .Union(roleClaims)
                .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpires),
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}
