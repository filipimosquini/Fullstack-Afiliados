﻿using Backend.Api.Bases;
using Backend.Core.Services.DataTransferObjects;
using Backend.Core.Services.Interfaces;
using Backend.Core.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[Route("api/sign-in")]
public class SingInController : MainController
{
    private readonly ISignInService _service;

    public SingInController(ISignInService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AuthenticationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignIn([FromBody] SignInViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return CustomResponseError(ModelState);
        }

        return CustomResponse(await _service.SignIn(viewModel));
    }
}