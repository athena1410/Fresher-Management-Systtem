using Application.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace FresherManagement.Api.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiVersion("2.0")]
    public class CandidateController : ControllerBase
    {
        /// <summary>
        /// Test xml comment
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns list of candidate</response>
        /// <response code="400">Noway, just for demonstration</response>
        [HttpGet]
        [Authorize(Roles = nameof(Roles.ClassAdmin))]
        [SwaggerOperation(Description = "List Catalog Brands 123")]
        public async Task<IActionResult> Index()
        {
            //throw new DomainException("Athena1410");
            return await Task.FromResult(Ok(new { Id = "Toanmk" }));
        }
    }
}
