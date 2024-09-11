using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ardalis.GuardClauses;
using AutoMapper;
using FresherManagement.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FresherManagement.Api.Controllers.v1
{
    [ApiController]
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

        private IMapper _mapper;
        protected IMapper Mapper
        {
            get
            {
                _mapper ??= Guard.Against.Null(HttpContext.RequestServices.GetService<IMapper>(), nameof(Mapper));
                return _mapper;
            }
        }

        protected string CurrentUser => _identityService?.GetUserIdentity();
    }
}
