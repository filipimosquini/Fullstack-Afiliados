using Backend.Core.Services.Contracts.ViewModels;
using Backend.Core.Services.ViewModels;
using Backend.UnitTest.Fixtures;
using FluentValidation.TestHelper;

namespace Backend.UnitTest.Services.Contracts.ViewModel;

[Trait("Contract Tests", "Financial Transaction import file view model Test")]
public class FinancialTransactionImportFileViewModelContractTest : IClassFixture<FileFixture>
{
    private readonly FileFixture _fileFixture;

    public FinancialTransactionImportFileViewModelContractTest(FileFixture fileFixture)
    {
        _fileFixture = fileFixture;
    }


    [Fact(DisplayName = "Should content file has no problems and expected view model contract valid")]
    public void Should_ContentFileHasNoProblems_Expected_ViewModelContractMustBeValid()
    {
        // Arrange
        var contract = new FinancialTransactionImportFileViewModelContract();
        var viewModel = new FinancialTransactionImportFileViewModel
        {
            File = _fileFixture.CreateFileWithoutErrors()
        };

        // Act
        var result = contract.TestValidate(viewModel);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Should content type in file has different to text/plain expected view model contract valid")]
    public void Should_ContentTypeInFileHasDifferentToTextPlain_Expected_ViewModelContractMustBeInvalid()
    {
        // Arrange
        var contract = new FinancialTransactionImportFileViewModelContract();
        var viewModel = new FinancialTransactionImportFileViewModel
        {
            File = _fileFixture.CreateFileWithErrors("application/json")
        };

        // Act
        var result = contract.TestValidate(viewModel);

        // Assert
        result.ShouldHaveAnyValidationError();
    }

    [Theory(DisplayName = "Should content type in file is null or empty expected view model contract valid")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Should_ContentTypeInFileIsNullOrEmpty_Expected_ViewModelContractMustBeInvalid(string contentType)
    {
        // Arrange
        var contract = new FinancialTransactionImportFileViewModelContract();
        var viewModel = new FinancialTransactionImportFileViewModel
        {
            File = _fileFixture.CreateFileWithErrors(contentType)
        };

        // Act
        var result = contract.TestValidate(viewModel);

        // Assert
        result.ShouldHaveAnyValidationError();
    }


    [Fact(DisplayName = "Should file is empty and expected view model contract invalid")]
    public void Should_FileIsEmpty_Expected_ViewModelContractMustBeInvalid()
    {
        // Arrange
        var contract = new FinancialTransactionImportFileViewModelContract();
        var viewModel = new FinancialTransactionImportFileViewModel
        {
            File = _fileFixture.CreateEmptyFile()
        };

        // Act
        var result = contract.TestValidate(viewModel);

        // Assert
        result.ShouldHaveAnyValidationError();
    }
}