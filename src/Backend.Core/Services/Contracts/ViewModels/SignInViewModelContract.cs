using Backend.Core.Resources;
using Backend.Core.Services.ViewModels;
using FluentValidation;

namespace Backend.Core.Services.Contracts.ViewModels;

public class SignInViewModelContract : AbstractValidator<SignInViewModel>
{
    public SignInViewModelContract()
    {
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().WithMessage(Messages.SignInViewModelContract_EmailIsRequired)
            .EmailAddress().WithMessage(Messages.SignInViewModelContract_EmailIsInvalid);

        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage(Messages.SignInViewModelContract_PasswordIsRequired)
            .Length(6, 15).WithMessage(Messages.SignInViewModelContract_PasswordMustHaveBetween_6_15_characteres);
    }
}