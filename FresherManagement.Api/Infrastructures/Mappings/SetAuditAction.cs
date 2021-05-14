using Application.Core.Commands;
using AutoMapper;
using FresherManagement.Api.Services;
using System;

namespace FresherManagement.Api.Infrastructures.Mappings
{
    public class SetAuditAction<T> : IMappingAction<object, Command<T>>
    {
        private readonly IIdentityService _identityService;

        public SetAuditAction(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public void Process(object source, Command<T> destination, ResolutionContext context)
        {
            destination.CreatedBy = _identityService.GetUserIdentity();
            destination.CreatedDate = DateTimeOffset.UtcNow;
        }
    }
}
