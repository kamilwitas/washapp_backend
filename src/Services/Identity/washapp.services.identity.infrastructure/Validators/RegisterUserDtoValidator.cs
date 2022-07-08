using washapp.services.identity.application.DTO;
using FluentValidation;
using washapp.services.identity.infrastructure.Data;

namespace washapp.services.identity.infrastructure.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(WashAppDbContext washAppDbContext)
        {
            RuleFor(x => x.Login)
                .Custom((value, context) =>
            {
                var user = washAppDbContext.Users.FirstOrDefault((x=>x.Login==value));
                if (user is not null)
                {
                    context.AddFailure("User with specified login already exists");
                }
            });
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("Firstname cannot be empty.");
            RuleFor(v => v.LastName).NotEmpty().WithMessage("LastName cannot be empty");
            RuleFor(b => b.PasswordConfirmation).Equal(n => n.Password).WithMessage("Password confirmation error.");
        }
    }
}
