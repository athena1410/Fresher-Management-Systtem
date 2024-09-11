using Application.Domain.Entities;
using Application.Domain.Exceptions;
using Common.Guard;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Commands.Role.CreateRole
{
    public class CreateRoleCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<CreateRoleCommandHandler> logger) : IRequestHandler<CreateRoleCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager = Guard.NotNull(userManager, nameof(userManager));
        private readonly ILogger<CreateRoleCommandHandler> _logger = Guard.NotNull(logger, nameof(logger));

        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                throw new NotFoundException($"User with Name {request.UserName} is not existed.");
            }

            IdentityResult result = await _userManager.AddToRolesAsync(user, request.Roles);
            if (result.Succeeded)
            {
                return Unit.Value;
            }

            _logger.LogError($"An error occurred while processing create new role for user {request.UserName}: {request}");
            throw new DomainException(result.Errors.Select(x => new ValidationFailure(x.Code, x.Description)));
        }
    }
}
