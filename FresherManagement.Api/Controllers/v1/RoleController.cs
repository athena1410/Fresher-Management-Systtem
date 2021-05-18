using Application.Core.Commands.Role.CreateRole;
using Application.Core.DTOs.Role;
using Common.Guard;
using FresherManagement.Api.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Role = Application.Core.Constants.Role;

namespace FresherManagement.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RoleController : BaseController
    {
        private readonly ILogger<RoleController> _logger;

        public RoleController(ILogger<RoleController> logger)
        {
            _logger = Guard.NotNull(logger, nameof(logger));
        }

        /// <summary>
        /// Assign new roles for user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("")]
        [AuthorizeRoles(Role.ADMINISTRATOR, Role.MANAGER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Description = "Assign Roles For User", OperationId = "role")]
        public async Task<IActionResult> CreateRolesAsync([FromBody] CreateRolesDto request)
        {
            var command = CreateRoleCommand.CreateFromInput(request, CurrentUser);
            return Ok(await Mediator.Send(command));
        }
    }
}
