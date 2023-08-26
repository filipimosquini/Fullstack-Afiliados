using Microsoft.AspNetCore.Http;

namespace Backend.Core.Services.ViewModels;

public class FinancialTransactionImportFileViewModel
{
    public IFormFile? File { get; set; }
}