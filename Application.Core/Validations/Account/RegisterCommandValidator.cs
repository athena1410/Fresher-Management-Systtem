using Application.Core.Commands.Account.Register;
using FluentValidation;

namespace Application.Core.Validations.Account
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.UserName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserName cannot be null or empty.");

            RuleFor(c => c.UserName)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(6)
                .WithMessage("UserName has minimum length is 6.");

            RuleFor(c => c.Email)
                .Cascade(CascadeMode.Stop)
                .EmailAddress();

            RuleFor(c => c.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password cannot be null or empty.");

            RuleFor(c => c.Password)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")
                .WithMessage("Password is invalid.");
        }
    }
}
