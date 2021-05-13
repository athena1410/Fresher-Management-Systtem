using Common.Guard;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Serilog;

namespace FresherManagement.Api.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = Guard.Null(context, nameof(context));
        }

        public string GetUserIdentity()
        {
            var result = _context.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub);
            return _context.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? "AnonymousUser";
        }
    }
}
