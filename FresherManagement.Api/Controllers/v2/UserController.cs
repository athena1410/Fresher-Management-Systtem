using System.Threading.Tasks;
using Application.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FresherManagement.Api.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiVersion("2.0")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Test xml comment
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns list of candidate</response>
        /// <response code="400">Noway, just for demonstration</response>
        [HttpGet]
        [SwaggerOperation(Description = "List Catalog Brands 123")]
        public async Task<IActionResult> Index()
        {
            //throw new DomainException("Athena1410");
            return await Task.FromResult(Ok(new { Id = "Toanmk" }));
        }
    }
}
