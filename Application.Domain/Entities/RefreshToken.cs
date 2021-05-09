using System;

namespace Application.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime CreatedDate { get; set; }
        public DateTime? RevokedDate { get; set; }
        public bool IsActive => !RevokedDate.HasValue && !IsExpired;
    }
}
