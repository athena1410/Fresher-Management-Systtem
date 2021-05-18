using Common.Guard;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FresherManagement.Api.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = Guard.NotNull(context, nameof(context));
        }

        public string GetUserIdentity()
        {
            return _context.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? "AnonymousUser";
        }
    }
}
