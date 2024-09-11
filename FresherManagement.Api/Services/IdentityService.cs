using Common.Guard;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FresherManagement.Api.Services
{
    public class IdentityService(IHttpContextAccessor context) : IIdentityService
    {
        private readonly IHttpContextAccessor _context = Guard.NotNull(context, nameof(context));

        public string GetUserIdentity()
        {
            return _context.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? "AnonymousUser";
        }
    }
}
