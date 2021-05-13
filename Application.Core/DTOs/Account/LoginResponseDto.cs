using System.Collections.Generic;
using Application.Core.Enums;

namespace Application.Core.DTOs.Account
{
    public class LoginResponseDto
    {
        public LoginStatus LoginStatus { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public static LoginResponseDto NotExisted(string message)
            => CreateFromInput(LoginStatus.NotExisted, message);

        public static LoginResponseDto EmailNotConfirmed(string message)
            => CreateFromInput(LoginStatus.EmailNotConfirmed, message);

        public static LoginResponseDto LockedOut(string message)
            => CreateFromInput(LoginStatus.LockedOut, message);

        public static LoginResponseDto RequiresTwoFactorAuthentication(string message)
            => CreateFromInput(LoginStatus.RequiresTwoFactorAuthentication, message);

        public static LoginResponseDto Failure(string message, List<string> errors = default)
            => CreateFromInput(LoginStatus.Failure, message, errors);

        public static LoginResponseDto CreateFromInput(LoginStatus status, string message, List<string> errors = default)
        {
            return new LoginResponseDto
            {
                LoginStatus = status,
                Message = message
            };
        }

    }
}
