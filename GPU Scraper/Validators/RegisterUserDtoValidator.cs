using FluentValidation;
using GPU_Scraper.Entities;
using GPUScraper.Models.Models;

namespace GPUScraper.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(GPUScraperDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("W celu rejestracji użytkownika wymagane jest podanie adresu e-mail.")
                .EmailAddress()
                .WithMessage("Podano nieprawidłowy format adresu e-mail.")
                .Custom((value, context) =>
                {
                    var isEmailInUse = dbContext.Users.Any(x => x.Email == value);
                    if (isEmailInUse)
                    {
                        context.AddFailure("Email", "Podany email jest zajęty.");
                    }
                });

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("W celu rejestracji użytkownika wymagane jest uzupełnienie nazwy użytkownika.")
                .MaximumLength(16)
                .WithMessage("Nazwa użytkownika nie może być dłuższa niż 16 znaków.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("W celu rejestracji użytkownika wymagane jest podanie hasła.")
                .MinimumLength(8)
                .WithMessage("Hasło musi zawierać co najmniej 8 znaków.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password)
                .WithMessage("Podane hasła nie są takie same.");
        }
    }
}
