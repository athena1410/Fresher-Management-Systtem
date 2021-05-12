using Application.Core.DTOs.Account;
using Application.Core.DTOs.Email;
using Application.Core.Enums;
using Application.Core.Interfaces.Services;
using Application.Domain.Exceptions;
using Common.Guard;
using Infrastructure.Identity.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JWTSettings> jwtOptions,
            IEmailService emailService,
            ILogger<AuthenticationService> logger
        )
        {
            _userManager = Guard.Null(userManager, nameof(userManager));
            _roleManager = Guard.Null(roleManager, nameof(roleManager));
            _signInManager = Guard.Null(signInManager, nameof(signInManager));
            _jwtSettings = Guard.Null(jwtOptions?.Value, nameof(jwtOptions));
            _emailService = Guard.Null(emailService, nameof(emailService));
            _logger = Guard.Null(logger, nameof(logger));
        }

        public async Task<bool> RegisterAsync(string userName, string email, string password)
        {
            ApplicationUser userWithEmail = await this._userManager.FindByEmailAsync(email);
            if (userWithEmail != null)
            {
                throw new DuplicateException($"User with email {email} is already existed.");
            }

            ApplicationUser userWithUserName = await this._userManager.FindByNameAsync(userName);
            if (userWithUserName != null)
            {
                throw new DuplicateException($"User with UserName {userName} is already existed.");
            }

            var user = new ApplicationUser
            {
                Email = email,
                UserName = userName
            };

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) throw new DomainException(result.ToString());

            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
            string confirmCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            _logger.LogInformation($"Generated confirm code : {confirmCode} with user {user.UserName}");

            return await _emailService.SendAsync(BuildVerificationEmail(user, confirmCode));
        }

        public async Task<bool> ConfirmEmailAsync(string userName, string code)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                throw new NotFoundException($"Can't get user with UserName is {userName}");
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return true;
            }

            throw new DomainException(result.ToString());
        }

        private EmailMessage BuildVerificationEmail(ApplicationUser user, string code)
        {
            var confirmLink = BuildConfirmLink(user, code);

            var mailMessage = new EmailMessage()
            {
                From = "duytoan.mk@gmail.com",
                Body = $"Please confirm your account by visiting this URL {confirmLink}",
                Subject = "Confirm Registration",
                To = user.Email
            };
            return mailMessage;
        }

        private string BuildConfirmLink(ApplicationUser user, string code)
        {
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/v1/account/confirm-email/";
            var verificationUri = QueryHelpers.AddQueryString(route, "userName", user.UserName);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }
    }
}
