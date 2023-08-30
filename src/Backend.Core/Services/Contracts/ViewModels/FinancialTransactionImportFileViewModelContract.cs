using Backend.Core.Resources;
using Backend.Core.Services.ViewModels;
using Backend.Infra.CrossCutting.Converters;
using FluentValidation;

namespace Backend.Core.Services.Contracts.ViewModels;

public class FinancialTransactionImportFileViewModelContract : AbstractValidator<FinancialTransactionImportFileViewModel>
{
    public FinancialTransactionImportFileViewModelContract()
    {
        RuleFor(x => x.EncodedFile)
            .Must(FileIsNullOrEmptyValidate).WithMessage(Messages.FinancialTransactionImportFileViewModelContract_FileIsRequired)
            .Must(ContentTypeIsPresentValidate).WithMessage(Messages.FinancialTransactionImportFileViewModelContract_ContentTypeIsRequired)
            .Must(ContentTypeIsTextPlainValidate).WithMessage(Messages.FinancialTransactionImportFileViewModelContract_FileMustBeTextFile);
    }

    protected bool ContentTypeIsPresentValidate(string? encodedFile)
    {
        var file = ConvertBase64ToFormFile.ConvertToFormFile(encodedFile);

        if (string.IsNullOrWhiteSpace(file?.ContentType))
        {
            return false;
        }

        return true;
    }

    protected bool ContentTypeIsTextPlainValidate(string? encodedFile)
    {
        var file = ConvertBase64ToFormFile.ConvertToFormFile(encodedFile);

        if (file?.ContentType?.ToLower() != "text/plain")
        {
            return false;
        }

        return true;
    }

    protected bool FileIsNullOrEmptyValidate(string? encodedFile)
    {
        var file = ConvertBase64ToFormFile.ConvertToFormFile(encodedFile);

        if (file is null || file?.Length == 0)
        {
            return false;
        }

        return true;
    }
}