using Backend.Api.Bases;
using Backend.Core.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[Route("api/transactions")]
public class FinancialTransactionController : MainController
{

    [HttpPost("import")]
    public async Task<IActionResult> Import(IFormFile file)
    {
        var viewModel = new FinancialTransactionImportFileViewModel
        {
            File = file
        };

        return Ok();
    }
}