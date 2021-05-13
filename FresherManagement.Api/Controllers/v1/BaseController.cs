using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ardalis.GuardClauses;
using FresherManagement.Api.Services;
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

        private IIdentityService _identityService;

        protected IIdentityService IdentityService
        {
            get
            {
                _identityService ??= Guard.Against.Null(HttpContext.RequestServices.GetService<IIdentityService>(), nameof(IdentityService));
                return _identityService;
            }
        }

        protected string CurrentUser => IdentityService?.GetUserIdentity();
    }
}
