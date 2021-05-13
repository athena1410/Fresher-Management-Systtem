using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Application.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; }
    }
}
