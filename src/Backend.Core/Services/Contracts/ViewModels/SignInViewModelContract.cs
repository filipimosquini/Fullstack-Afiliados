using Backend.Core.Services.ViewModels;
using FluentValidation;

namespace Backend.Core.Services.Contracts.ViewModels;

public class SignInViewModelContract : AbstractValidator<SignInViewModel>
{
    public SignInViewModelContract()
    {
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().WithMessage("The field e-mail is required")
            .EmailAddress().WithMessage("The field e-mail is invalid");

        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("The field password is required")
            .Length(6, 15).WithMessage("A password must have between 6 a 15 characters");
    }
}