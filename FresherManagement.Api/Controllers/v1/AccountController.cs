using System.Net;
using System.Threading.Tasks;
using Application.Core.Commands.Account.ConfirmEmail;
using Application.Core.Commands.Account.Login;
using Application.Core.Commands.Account.Register;
using Application.Core.Commands.Account.Role;
using Application.Core.DTOs.Account;
using Application.Core.Enums;
using Common.Guard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

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
            var command = RegisterCommand.CreateFromInput(request, CurrentUser);
            await Mediator.Send(command);
            return Ok();
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
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto request)
        {
            var command = LoginCommand.CreateFromInput(request, CurrentUser);
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Create new roles for user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("role")]
        [Authorize(Roles = nameof(Roles.User))]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation(Description = "Add Role For User", OperationId = "role")]
        public async Task<IActionResult> CreateRolesAsync([FromBody] CreateRolesRequest request)
        {
            var command = CreateRoleCommand.CreateFromInput(request, CurrentUser);
            return Ok(await Mediator.Send(command));
        }
    }
}
