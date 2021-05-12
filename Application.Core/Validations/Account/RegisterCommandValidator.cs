using Application.Core.Commands.Account.Register;
using FluentValidation;

namespace Application.Core.Validations.Account
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserName cannot be null or empty.");

            RuleFor(c => c.UserName)
                .MinimumLength(6)
                .WithMessage("UserName has minimum length is 6.");

            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email cannot be null or empty.");

            RuleFor(c => c.Email)
                .Matches(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")
                .WithMessage("Email format not valid");

            RuleFor(c => c.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password cannot be null or empty.");

            RuleFor(c => c.Password)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")
                .WithMessage("Password format not valid");
        }
    }
}
