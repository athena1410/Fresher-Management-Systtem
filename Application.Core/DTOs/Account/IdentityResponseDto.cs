using System;
using System.Collections.Generic;
using Application.Core.Enums;
using Newtonsoft.Json;

namespace Application.Core.DTOs.Account
{
    public class IdentityResponseDto
    {
        public LoginStatus Status { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiration { get; set; }
        public string Message { get; set; }

        public static IdentityResponseDto NotExisted(string message)
            => CreateFromInput(LoginStatus.NotExisted, message);

        public static IdentityResponseDto EmailNotConfirmed(string message)
            => CreateFromInput(LoginStatus.EmailNotConfirmed, message);

        public static IdentityResponseDto LockedOut(string message)
            => CreateFromInput(LoginStatus.LockedOut, message);

        public static IdentityResponseDto RequiresTwoFactorAuthentication(string message)
            => CreateFromInput(LoginStatus.RequiresTwoFactorAuthentication, message);

        public static IdentityResponseDto Failure(string message, List<string> errors = default)
            => CreateFromInput(LoginStatus.Failure, message, errors);

        public static IdentityResponseDto CreateFromInput(LoginStatus status, string message, List<string> errors = default)
        {
            return new IdentityResponseDto
            {
                Status = status,
                Message = message
            };
        }

    }
}
