using Application.Core.DTOs.Account;
using Application.Core.Interfaces.Services;
using Application.Domain.Entities;
using Common.Guard;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.Account.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly ITokenClaimService _tokenClaimService;

        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            ITokenClaimService tokenClaimService,
            ILogger<LoginCommandHandler> logger)
        {
            this._userManager = Guard.Null(userManager, nameof(userManager));
            this._tokenClaimService = Guard.Null(tokenClaimService, nameof(tokenClaimService));
            this._logger = Guard.Null(logger, nameof(logger));
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return LoginResponseDto.NotExisted($"User with {request.UserName} not existed.");
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return LoginResponseDto.EmailNotConfirmed($"You need to confirm your email.");
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                return LoginResponseDto.LockedOut($"Account has been locked.");
            }

            if (await _userManager.CheckPasswordAsync(user, request.Password))
            {
                JwtSecurityToken jwtSecurityToken = await _tokenClaimService.GetTokenAsync(user);
                return new LoginResponseDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                    Message = "Success."
                };
            }

            return LoginResponseDto.Failure($"Incorrect Credentials for user {user.UserName}."); ;
        }
    }
}
