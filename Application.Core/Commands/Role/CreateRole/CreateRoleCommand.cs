using System;
using MediatR;
using System.Collections.Generic;
using Application.Core.DTOs.Role;

namespace Application.Core.Commands.Role.CreateRole
{
    public class CreateRoleCommand : Command<Unit>
    {
        public string UserName { get; private init; }
        public List<string> Roles { get; private init; }

        public static CreateRoleCommand CreateFromInput(CreateRolesDto request, string createdBy)
            => new CreateRoleCommand
            {
                UserName = request.UserName,
                Roles = request.Roles,
                CreatedBy = createdBy,
                CreatedDate = DateTimeOffset.Now
            };
    }
}
