using Backend.Core.Bases;
using Backend.Core.Services.Contracts.ViewModels;
using Backend.Core.Services.Interfaces;
using Backend.Core.Services.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Backend.Core.Services;

public class UserService : BaseService, IUserService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAuthenticationService _authenticationService;

    private readonly UserViewModelContract _userViewModelContract;

    public UserService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IAuthenticationService authenticationService, UserViewModelContract userViewModelContract)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _authenticationService = authenticationService;
        _userViewModelContract = userViewModelContract;
    }

    public async Task<CustomValidationResult> AddUser(UserViewModel viewModel)
    {
        var validateViewModel = _userViewModelContract.Validate(viewModel);

        if (!validateViewModel.IsValid)
        {
            AddErrors(validateViewModel.Errors);
            return CustomValidationResult;
        }

        var user = new IdentityUser
        {
            UserName = viewModel.Email,
            Email = viewModel.Email,
            EmailConfirmed = true
        };

        var createdUser = await _userManager.CreateAsync(user, viewModel.Password);

        if (!createdUser.Succeeded)
        {
            AddErrors(createdUser.Errors.Select(x => x.Description));
            return CustomValidationResult;
        }

        await _signInManager.SignInAsync(user, false);

        CustomValidationResult.Data = await _authenticationService.GenerateJwtToken(viewModel.Email);

        return CustomValidationResult;
    }
}