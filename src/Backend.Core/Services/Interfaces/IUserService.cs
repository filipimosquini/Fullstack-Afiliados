using Backend.Core.Bases;
using Backend.Core.Services.ViewModels;

namespace Backend.Core.Services.Interfaces;

public interface IUserService
{
    Task<CustomValidationResult> AddUserAsync(UserViewModel viewModel);
}