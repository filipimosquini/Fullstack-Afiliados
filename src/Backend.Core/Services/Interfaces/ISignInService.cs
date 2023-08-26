using Backend.Core.Bases;
using Backend.Core.Services.ViewModels;

namespace Backend.Core.Services.Interfaces;

public interface ISignInService
{
    Task<CustomValidationResult> SignIn(SignInViewModel viewModel);
}