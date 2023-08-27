using Backend.Api.Bases;
using Backend.Core.Services.Interfaces;
using Backend.Core.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[Route("api/transactions")]
public class FinancialTransactionController : MainController
{

    private readonly IFinancialTransactionService _service;

    public FinancialTransactionController(IFinancialTransactionService service)
    {
        _service = service;
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportAsync(IFormFile file)
    {
        var viewModel = new FinancialTransactionImportFileViewModel
        {
            File = file
        };

        if (!ModelState.IsValid)
        {
            return CustomResponseError(ModelState);
        }

        return CustomResponse(await _service.ImportFinancialTransactionFileAsync(viewModel));
    }
}