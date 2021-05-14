using Application.Core.Commands.Account.ConfirmEmail;
using Application.Core.Commands.Account.Login;
using Application.Core.Commands.Account.RefreshToken;
using Application.Core.Commands.Account.Register;
using Application.Core.DTOs.Account;
using Common.Guard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Application.Core.Commands.Account.RevokeToken;

namespace FresherManagement.Api.Controllers.v1
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = Guard.Null(logger, nameof(logger));
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation(Description = "Register new user", OperationId = "register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto request)
        {
            var command = Mapper.Map<RegisterCommand>(request);
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Confirm Email
        /// </summary>
        /// <returns></returns>
        [HttpGet("confirm-email")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation(Description = "Confirm Email", OperationId = "confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userName, [FromQuery] string code)
        {
            var command = ConfirmEmailCommand.CreateFromInput(userName, code, CurrentUser);
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation(Description = "Login", OperationId = "login")]
        public async Task<IActionResult> LoginAsync([FromBody] IdentityRequestDto request)
        {
            var command = Mapper.Map<LoginCommand>(request);
            var result = await Mediator.Send(command);
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            }
            return Ok(result);
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation(Description = "Use refresh token to get new access token", OperationId = "refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var refreshToken = Request.Cookies["refresh-token"];
            var command = RefreshTokenCommand.CreateFromInput(refreshToken, CurrentUser);
            IdentityResponseDto result = await Mediator.Send(command);
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            }

            return Ok(result);
        }

        /// <summary>
        /// Revoke Token
        /// </summary>
        /// <returns></returns>
        [HttpPost("revoke-token")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation(Description = "Revoking A Refresh Token", OperationId = "revoke-token")]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeTokenRequestDto request)
        {
            var token = string.IsNullOrEmpty(request.Token)
                ? Request.Cookies["refresh-token"]
                : HttpUtility.UrlDecode(request.Token);

            var command = RevokeTokenCommand.CreateFromInput(token, CurrentUser);
            return Ok(await Mediator.Send(command));
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTimeOffset expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = expires,
            };
            Response.Cookies.Append("refresh-token", refreshToken, cookieOptions);
        }
    }
}
