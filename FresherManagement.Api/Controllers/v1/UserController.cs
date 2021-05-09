using System.Net;
using System.Threading.Tasks;
using Application.Core.Commands.User.CreateUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace FresherManagement.Api.Controllers.v1
{
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogError("Xin chao");
            return await Task.FromResult(Ok("Xin chao"));
        }

        /// <summary>
        /// Register new User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerOperation(Description = "List Catalog Brands 123", OperationId = "hehehe")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand request)
        {
            var result = await Mediator.Send(request);
            _logger.LogError($"register new user {result}");
            return await Task.FromResult(Ok("Xin chao"));
        }
    }
}
