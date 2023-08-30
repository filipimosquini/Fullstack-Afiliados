using Backend.Core.Resources;
using Backend.Core.Services.ViewModels;
using FluentValidation;

namespace Backend.Core.Services.Contracts.ViewModels;

public class FinancialTransactionImportFileViewModelContract : AbstractValidator<FinancialTransactionImportFileViewModel>
{
    public FinancialTransactionImportFileViewModelContract()
    {
        RuleFor(x => x.EncodedFile)
            .Must(FileIsNullOrEmptyValidate)
            .WithMessage(Messages.FinancialTransactionImportFileViewModelContract_FileIsRequired);

        RuleFor(x => x.ContentType)
            .Must(ContentTypeIsPresentValidate).WithMessage(Messages.FinancialTransactionImportFileViewModelContract_ContentTypeIsRequired)
            .Must(ContentTypeIsTextPlainValidate).WithMessage(Messages.FinancialTransactionImportFileViewModelContract_FileMustBeTextFile);
    }

    protected bool ContentTypeIsPresentValidate(string? contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
        {
            return false;
        }

        return true;
    }

    protected bool ContentTypeIsTextPlainValidate(string? contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType) || contentType?.ToLower() != "text/plain")
        {
            return false;
        }

        return true;
    }

    protected bool FileIsNullOrEmptyValidate(string? encodedFile)
    {
        if (string.IsNullOrWhiteSpace(encodedFile))
        {
            return false;
        }

        return true;
    }
}