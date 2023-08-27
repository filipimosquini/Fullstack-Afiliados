using Backend.Api.Bases;
using Backend.Core.Services.DataTransferObjects;
using Backend.Core.Services.Interfaces;
using Backend.Core.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[Route("api/users")]
public class UserController : MainController
{
    private readonly IUserService _service;

    public UserController(IUserService userService)
    {
        _service = userService;
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns> Informations about token JWT </returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AuthenticationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddUserAsync([FromBody] UserViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return CustomResponseError(ModelState);
        }

        return CustomResponse(await _service.AddUserAsync(viewModel));
    }
}