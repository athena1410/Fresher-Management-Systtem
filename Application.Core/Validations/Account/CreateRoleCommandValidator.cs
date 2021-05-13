using System;
using System.Linq;
using Application.Core.Commands.Account.Role;
using Application.Core.Enums;
using FluentValidation;

namespace Application.Core.Validations.Account
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
                .Must(x => x.All(inputRole => Enum.GetNames(typeof(Roles)).Any(role => role == inputRole)))
                .WithMessage($"One or more role is invalid.");
        }
    }
}
