using FluentValidation;
using GPUScraper.Models.Models;

namespace GPUScraper.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Aby się zalogować należy podać adres e-mail użytkownika.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Aby się zalogować należy podać hasło użytkownika.");
        }
    }
}
