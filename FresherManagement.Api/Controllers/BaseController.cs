using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;

namespace FresherManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
