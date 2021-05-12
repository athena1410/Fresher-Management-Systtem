using System.Net;
using System.Threading.Tasks;
using Application.Core.Commands.Account.ConfirmEmail;
using Application.Core.Commands.Account.Login;
using Application.Core.Commands.Account.Register;
using Application.Core.DTOs.Account;
using Common.Guard;
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
            var command = RegisterCommand.CreateFromInput(request);
            command.CreatedBy = "Athena1410";

            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userName, [FromQuery] string code)
        {
            var command = ConfirmEmailCommand.CreateCommandFromInput(userName, code);
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
