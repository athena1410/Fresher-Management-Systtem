using Application.Core.Commands;
using AutoMapper;
using FresherManagement.Api.Services;
using System;

namespace FresherManagement.Api.Infrastructures.Mappings
{
    public class SetAuditAction<T>(IIdentityService identityService) : IMappingAction<object, Command<T>>
    {
        private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

        public void Process(object source, Command<T> destination, ResolutionContext context)
        {
            destination.CreatedBy = _identityService.GetUserIdentity();
            destination.CreatedDate = DateTimeOffset.UtcNow;
        }
    }

    public class SetAuditAction(IIdentityService identityService) : IMappingAction<object, Command>
    {
        private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

        public void Process(object source, Command destination, ResolutionContext context)
        {
            destination.CreatedBy = _identityService.GetUserIdentity();
            destination.CreatedDate = DateTimeOffset.UtcNow;
        }
    }
}
