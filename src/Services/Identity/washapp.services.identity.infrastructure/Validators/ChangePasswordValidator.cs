using washapp.services.identity.application.Commands;
using FluentValidation;

namespace washapp.services.identity.infrastructure.Validators;

public class ChangePasswordValidator : AbstractValidator<ChangePassword>
{
    public ChangePasswordValidator()
    {
        RuleFor(c => c.NewPassword).Equal(v => v.NewPasswordConfirmation).WithMessage("Confirmation password error.");
        RuleFor(b => b.NewPassword).NotEmpty().WithMessage("Password cannot be empty.");
    }
}