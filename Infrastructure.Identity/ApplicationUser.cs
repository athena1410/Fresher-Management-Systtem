using System.Collections.Generic;
using Application.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; }
    }
}
