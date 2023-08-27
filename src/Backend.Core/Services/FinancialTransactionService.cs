﻿using Backend.Core.Bases;
using Backend.Core.Bases.Interfaces;
using Backend.Core.Entities;
using Backend.Core.Services.Contracts.Business;
using Backend.Core.Services.Contracts.ViewModels;
using Backend.Core.Services.DataTransferObjects;
using Backend.Core.Services.Interfaces;
using Backend.Core.Services.ViewModels;

namespace Backend.Core.Services;

public class FinancialTransactionService : BaseService, IFinancialTransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly FinancialTransactionBusinessContract _financialTransactionBusinessContract;
    private readonly FinancialTransactionImportFileViewModelContract _financialTransactionImportFileViewModelContract;
    private readonly FinancialTransactionFileStructureBusinessContract _financialTransactionFileStructureBusinessContract;
    
    public FinancialTransactionService(IUnitOfWork unitOfWork, IFileService fileService, FinancialTransactionBusinessContract financialTransactionBusinessContract, FinancialTransactionImportFileViewModelContract financialTransactionImportFileViewModelContract, FinancialTransactionFileStructureBusinessContract financialTransactionFileStructureBusinessContract)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _financialTransactionBusinessContract = financialTransactionBusinessContract;
        _financialTransactionImportFileViewModelContract = financialTransactionImportFileViewModelContract;
        _financialTransactionFileStructureBusinessContract = financialTransactionFileStructureBusinessContract;
    }

    public virtual Task ValidateFinancialTransactionsAsync(IEnumerable<FinancialTransaction> transactions)
    {
        var index = 0;
        foreach (var transaction in transactions)
        {
            index++;

            var validate = _financialTransactionBusinessContract.Validate(transaction);

            if (!validate.IsValid)
            {
                validate.Errors.ForEach(error =>
                {
                    error.ErrorMessage = string.Format(error.ErrorMessage, index);

                    AddError(error.ErrorMessage);
                });
            }
        }

        return Task.CompletedTask;
    }

    public virtual async Task<ICollection<FinancialTransaction>> CreateFinancialTransactionsAsync(IEnumerable<TransactionDto> transactions)
    {
        var financialTransactions = new List<FinancialTransaction>();

        foreach (var transaction in transactions)
        {
            var product = await _unitOfWork.ProductRepository.FindByDescription(transaction.Product);
            var seller = await _unitOfWork.SellerRepository.FindByName(transaction.Seller);
            var financialTransactionType = await _unitOfWork.FinancialTransactionTypeRepository.FindById(transaction.FinancialTransactionType);

            financialTransactions.Add(new FinancialTransaction
            {
                Date = transaction.Date.Value,
                Value = transaction.Value,
                Seller = seller,
                Product = product,
                FinancialTransactionType = financialTransactionType
            });
        }

        return financialTransactions;
    }

    public async Task<CustomValidationResult> ImportFinancialTransactionFileAsync(FinancialTransactionImportFileViewModel viewModel)
    {
        var validateViewModel = _financialTransactionImportFileViewModelContract.Validate(viewModel);

        if (!validateViewModel.IsValid)
        {
            AddErrors(validateViewModel.Errors);
            return CustomValidationResult;
        }

        var validateFileContent =
            await _financialTransactionFileStructureBusinessContract.ValidateAsync(viewModel.File);

        if (!validateFileContent.IsValid)
        {
            AddErrors(validateFileContent.Errors);
            return CustomValidationResult;
        }

        var transactionsDto = await _fileService.ExtractDataFromFileAsync(viewModel.File);

        var transactions = await CreateFinancialTransactionsAsync(transactionsDto);

        await ValidateFinancialTransactionsAsync(transactions);

        if (!CustomValidationResult.IsValid)
        {
            return CustomValidationResult;
        }

        await _unitOfWork.FinancialTransactionRepository.RemoveAll();

        await _unitOfWork.FinancialTransactionRepository.AddRangeAsync(transactions);

        await _unitOfWork.SaveAsync();

        CustomValidationResult.Data = "Transactions import was successfully";

        return CustomValidationResult;
    }
}