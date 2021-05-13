using Common.Guard;
using Microsoft.AspNetCore.Http;

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
            return _context.HttpContext?.User.FindFirst("sub")?.Value ?? "AnonymousUser";
        }
    }
}
