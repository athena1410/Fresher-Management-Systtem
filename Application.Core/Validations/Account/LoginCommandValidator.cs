using Application.Core.Commands.Account.Login;
using FluentValidation;

namespace Application.Core.Validations.Account
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.UserName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserName cannot be null or empty.");

            RuleFor(c => c.UserName)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(6)
                .WithMessage("UserName should contain at least 6 characters.");

            RuleFor(c => c.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password cannot be null or empty.");

            RuleFor(c => c.Password)
                .Cascade(CascadeMode.Stop)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")
                .WithMessage("Password is invalid.");
        }
    }
}
