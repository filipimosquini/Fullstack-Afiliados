﻿using Backend.Core.Bases;
using Backend.Core.Services.DataTransferObjects;
using Backend.Core.Services.ViewModels;

namespace Backend.Core.Services.Interfaces;

public interface IFinancialTransactionService
{
    Task<CustomValidationResult> ImportFinancialTransactionFileAsync(FinancialTransactionImportFileViewModel viewModel);
    Task<IEnumerable<ImportedTransactionsDto>> GetImportedTransactionsAsync();
}