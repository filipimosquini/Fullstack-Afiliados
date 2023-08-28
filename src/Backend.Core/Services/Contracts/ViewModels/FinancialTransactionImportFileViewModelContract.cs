using Backend.Core.Resources;
using Backend.Core.Services.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Backend.Core.Services.Contracts.ViewModels;

public class FinancialTransactionImportFileViewModelContract : AbstractValidator<FinancialTransactionImportFileViewModel>
{
    public FinancialTransactionImportFileViewModelContract()
    {
        RuleFor(x => x.File)
            .Must(FileIsNullOrEmptyValidate).WithMessage(Messages.FinancialTransactionImportFileViewModelContract_FileIsRequired)
            .Must(ContentTypeIsPresentValidate).WithMessage(Messages.FinancialTransactionImportFileViewModelContract_ContentTypeIsRequired)
            .Must(ContentTypeIsTextPlainValidate).WithMessage(Messages.FinancialTransactionImportFileViewModelContract_FileMustBeTextFile);
    }

    protected bool ContentTypeIsPresentValidate(IFormFile? file)
    {
        if (string.IsNullOrWhiteSpace(file?.ContentType))
        {
            return false;
        }

        return true;
    }

    protected bool ContentTypeIsTextPlainValidate(IFormFile? file)
    {

        if (file?.ContentType?.ToLower() != "text/plain")
        {
            return false;
        }

        return true;
    }

    protected bool FileIsNullOrEmptyValidate(IFormFile? file)
    {
        if (file is null || file?.Length == 0)
        {
            return false;
        }

        return true;
    }
}