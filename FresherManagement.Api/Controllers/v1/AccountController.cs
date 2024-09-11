using Application.Core.Commands.Account.ConfirmEmail;
using Application.Core.Commands.Account.Login;
using Application.Core.Commands.Account.RefreshToken;
using Application.Core.Commands.Account.Register;
using Application.Core.Commands.Account.RevokeToken;
using Application.Core.DTOs.Account;
using Common.Guard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using System.Web;

namespace FresherManagement.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController(ILogger<AccountController> logger) : BaseController
    {
        private readonly ILogger<AccountController> _logger = Guard.NotNull(logger, nameof(logger));

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Description = "Register new user", OperationId = "register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto request)
        {
            var command = Mapper.Map<RegisterCommand>(request);
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Confirm Email
        /// </summary>
        /// <returns></returns>
        [HttpGet("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Description = "Confirm Email", OperationId = "confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userName, [FromQuery] string code)
        {
            var command = ConfirmEmailCommand.CreateFromInput(userName, code, CurrentUser);
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Description = "Login", OperationId = "login")]
        public async Task<IActionResult> LoginAsync([FromBody] IdentityDto request)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Description = "Use refresh token to get new access token", OperationId = "refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var refreshToken = Request.Cookies["refresh-token"];
            var command = RefreshTokenCommand.CreateFromInput(refreshToken, CurrentUser);
            var result = await Mediator.Send(command);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Description = "Revoking A Refresh Token", OperationId = "revoke-token")]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeTokenDto request)
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
