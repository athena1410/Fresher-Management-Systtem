using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;

namespace FresherManagement.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator
        {
            get
            {
                _mediator ??= Guard.Against.Null(HttpContext.RequestServices.GetService<IMediator>(), nameof(Mediator));
                return _mediator;
            }
        }
    }
}
