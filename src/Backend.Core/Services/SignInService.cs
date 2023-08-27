using Backend.Core.Bases;
using Backend.Core.Services.Contracts.ViewModels;
using Backend.Core.Services.Interfaces;
using Backend.Core.Services.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Backend.Core.Services;

public class SignInService : BaseService, ISignInService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IAuthenticationService _authenticationService;
    private readonly SignInViewModelContract _signInViewModelContract;

    public SignInService(SignInManager<IdentityUser> signInManager, IAuthenticationService authenticationService, SignInViewModelContract signInViewModelContract)
    {
        _signInManager = signInManager;
        _authenticationService = authenticationService;
        _signInViewModelContract = signInViewModelContract;
    }

    public async Task<CustomValidationResult> SignInAsync(SignInViewModel viewModel)
    {
        var validateViewModel = _signInViewModelContract.Validate(viewModel);

        if (!validateViewModel.IsValid)
        {
            AddErrors(validateViewModel.Errors);
            return CustomValidationResult;
        }

        var resultado = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, false, true);

        if (resultado.IsLockedOut)
        {
            AddError("User temporarily blocked for invalid attempt");
        }

        if (resultado.IsNotAllowed)
        {
            AddError("Sign in not allowed");
        }

        if (!resultado.Succeeded)
        {
            AddError("Email or password are incorrect");
        }

        if (!CustomValidationResult.IsValid)
        {
            return CustomValidationResult;
        }

        CustomValidationResult.Data = await _authenticationService.GenerateJwtToken(viewModel.Email);

        return CustomValidationResult;
    }
}