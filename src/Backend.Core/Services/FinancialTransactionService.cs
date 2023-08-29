using Backend.Core.Bases;
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
    private readonly FinancialTransactionFileContentBusinessContract _financialTransactionFileContentBusinessContract;
    
    public FinancialTransactionService(IUnitOfWork unitOfWork, IFileService fileService, FinancialTransactionBusinessContract financialTransactionBusinessContract, FinancialTransactionImportFileViewModelContract financialTransactionImportFileViewModelContract, FinancialTransactionFileContentBusinessContract financialTransactionFileContentBusinessContract)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _financialTransactionBusinessContract = financialTransactionBusinessContract;
        _financialTransactionImportFileViewModelContract = financialTransactionImportFileViewModelContract;
        _financialTransactionFileContentBusinessContract = financialTransactionFileContentBusinessContract;
    }

    public virtual Task<bool> ValidateFinancialTransactionsAsync(IEnumerable<FinancialTransaction> transactions)
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

        return Task.FromResult(CustomValidationResult.IsValid);
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
            await _financialTransactionFileContentBusinessContract.ValidateAsync(viewModel.File);

        if (!validateFileContent.IsValid)
        {
            AddErrors(validateFileContent.Errors);
            return CustomValidationResult;
        }

        var transactionsDto = await _fileService.ExtractDataFromFileAsync(viewModel.File);

        var transactions = await CreateFinancialTransactionsAsync(transactionsDto);

        var hasValidTransactions = await ValidateFinancialTransactionsAsync(transactions);

        if (!hasValidTransactions)
        {
            return CustomValidationResult;
        }

        await _unitOfWork.FinancialTransactionRepository.RemoveAll();

        await _unitOfWork.FinancialTransactionRepository.AddRangeAsync(transactions);

        await _unitOfWork.SaveAsync();

        CustomValidationResult.Data = "Transactions import was successfully";

        return CustomValidationResult;
    }

    public async Task<IEnumerable<ImportedTransactionsDto>> GetImportedTransactionsAsync()
    {
        var collection = await _unitOfWork.FinancialTransactionRepository.GetImportedTransactionsAsync();

        return collection
            .GroupBy(x => new { x.Seller })
            .OrderBy(x => x.Key.Seller.Name)
            .Select(x => new ImportedTransactionsDto
            {
                Id = x.Key.Seller.Id,
                SellerName = x.Key.Seller.Name,
                Total = (x.Where(x => x.FinancialTransactionType.Signal == "+")
                            .Sum(z => z.Value) - 
                         x.Where(x => x.FinancialTransactionType.Signal == "-")
                             .Sum(z => z.Value)),
                Details = x.Select(x => new ImportedTransactionsDetailsDto()
                {
                    FinancialTransactionTypeDescription = x.FinancialTransactionType.Description,
                    FinancialTransactionTypeNature = x.FinancialTransactionType.Nature,
                    FinancialTransactionTypeSignal = x.FinancialTransactionType.Signal,
                    Product = x.Product.Description,
                    Value = x.Value
                })
            });
    }
}