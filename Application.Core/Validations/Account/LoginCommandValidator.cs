using Application.Core.Commands.Account.Login;
using FluentValidation;

namespace Application.Core.Validations.Account
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserName cannot be null or empty.");

            RuleFor(c => c.UserName)
                .MinimumLength(6)
                .WithMessage("UserName should contain at least 6 characters.");

            RuleFor(c => c.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password cannot be null or empty.");

            RuleFor(c => c.Password)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")
                .WithMessage("Password format not valid.");
        }
    }
}
