using Backend.Api.Bases;
using Backend.Core.Bases;
using Backend.Core.Services.DataTransferObjects;
using Backend.Core.Services.Interfaces;
using Backend.Core.Services.ViewModels;
using Backend.Infra.CrossCutting.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[Authorize]
[Route("api/financial-transactions")]
public class FinancialTransactionController : MainController
{

    private readonly IFinancialTransactionService _service;

    public FinancialTransactionController(IFinancialTransactionService service)
    {
        _service = service;
    }

    /// <summary>
    /// Import transactions from file
    /// </summary>
    /// <param name="EncodedFile"> Get base 64 encrypted file </param>
    /// <param name="ContentType"> Get content type file </param>
    /// <returns> Succcessfully message ou errors list </returns>
    [HttpPost("import")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CustomValidationResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ImportAsync(FinancialTransactionImportFileViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return CustomResponseError(ModelState);
        }

        return CustomResponse(await _service.ImportFinancialTransactionFileAsync(viewModel));
    }

    /// <summary>
    /// Get imported transactions
    /// </summary>
    /// <returns> Return list of imported transactions </returns>
    [HttpGet]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ImportedTransactionsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetImportedTransactionsAsync()
    {
        if (!ModelState.IsValid)
        {
            return CustomResponseError(ModelState);
        }

        return CustomResponse(await _service.GetImportedTransactionsAsync());
    }

}