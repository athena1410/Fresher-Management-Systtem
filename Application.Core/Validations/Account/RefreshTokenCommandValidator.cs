using Application.Core.Commands.Account.RefreshToken;
using FluentValidation;

namespace Application.Core.Validations.Account
{
    public class RefreshTokenCommandValidator: AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.Token)
                .Cascade(CascadeMode.Stop)
                .NotNull().NotEmpty()
                .WithMessage("Refresh token cannot be null or empty.");
        }
    }
}
