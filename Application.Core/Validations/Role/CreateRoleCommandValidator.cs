using Application.Core.Commands.Role.CreateRole;
using FluentValidation;
using System;
using System.Linq;

namespace Application.Core.Validations.Role
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(c => c.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserName cannot be null or empty.");

            RuleFor(c => c.UserName)
                .MinimumLength(6)
                .WithMessage("UserName should contain at least 6 characters.");

            RuleFor(x => x.Roles)
                .Must(x => x.All(inputRole => Enum.GetNames(typeof(Enums.Role)).Any(role => role == inputRole)))
                .WithMessage($"One or more role is invalid.");
        }
    }
}
