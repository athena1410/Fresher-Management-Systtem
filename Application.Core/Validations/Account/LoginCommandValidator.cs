using Application.Core.Commands.Account.Login;
using FluentValidation;

namespace Application.Core.Validations.Account
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
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
