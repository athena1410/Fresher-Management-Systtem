using Application.Core.Commands.Account.RevokeToken;
using FluentValidation;

namespace Application.Core.Validations.Account
{
    public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
    {
        public RevokeTokenCommandValidator()
        {
            RuleFor(x => x.Token)
                .Cascade(CascadeMode.Stop)
                .NotNull().NotEmpty()
                .WithMessage("Revoke token cannot be null or empty.");
        }
    }
}
