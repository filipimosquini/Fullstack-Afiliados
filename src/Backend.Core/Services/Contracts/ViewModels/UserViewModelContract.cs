using Backend.Core.Resources;
using Backend.Core.Services.ViewModels;
using FluentValidation;

namespace Backend.Core.Services.Contracts.ViewModels;

public class UserViewModelContract : AbstractValidator<UserViewModel>
{
    public UserViewModelContract()
    {
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().WithMessage(Messages.UserViewModelContract_EmailIsRequired)
            .EmailAddress().WithMessage(Messages.UserViewModelContract_EmailIsInvalid);

        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage(Messages.UserViewModelContract_PasswordIsRequired)
            .Length(6, 15).WithMessage(Messages.UserViewModelContract_PasswordMustHaveBetween_6_15_characteres);

        RuleFor(x => x.ConfirmPassword)
            .Must((model, field) => model.Password.Equals(field))
            .WithMessage(Messages.UserViewModelContract_PasswordsAreDifferent);
    }
}