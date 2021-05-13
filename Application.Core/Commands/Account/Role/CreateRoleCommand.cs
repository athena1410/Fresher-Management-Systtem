using System;
using MediatR;
using System.Collections.Generic;
using Application.Core.DTOs.Account;

namespace Application.Core.Commands.Account.Role
{
    public class CreateRoleCommand : Command<Unit>, IRequest<Unit>
    {
        public string UserName { get; private init; }
        public List<string> Roles { get; private init; }

        public static CreateRoleCommand CreateFromInput(CreateRolesRequest request, string createdBy)
            => new CreateRoleCommand
            {
                UserName = request.UserName,
                Roles = request.Roles,
                CreatedBy = createdBy,
                CreatedDate = DateTimeOffset.Now
            };
    }
}
