using Backend.Core.Services.Contracts.Business;
using Backend.UnitTest.Fixtures;
using FluentValidation.TestHelper;

namespace Backend.UnitTest.Services.Contracts.Business;

[Trait("Contract Tests", "Financial Transaction File Structure Test")]
public class FinancialTransactionFileContentBusinessContractTest : IClassFixture<FileFixture>
{
    private readonly FileFixture _fileFixture;

    public FinancialTransactionFileContentBusinessContractTest(FileFixture fileFixture)
    {
        _fileFixture = fileFixture;
    }

    [Fact(DisplayName = "Should content file has no problems and expected business contract valid")]
    public async Task Should_ContentFileHasNoProblems_Expected_ContractMustBeValid()
    {
        // Arrange
        var contract = new FinancialTransactionFileContentBusinessContract();
        var file = _fileFixture.CreateFileWithoutErrors();

        // Act
        var result = await contract.TestValidateAsync(file);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Should content file has problems and expected business contract invalid")]
    public async Task Should_ContentFileHasProblems_Expected_ContractMustBeInvalid()
    {
        // Arrange
        var contract = new FinancialTransactionFileContentBusinessContract();
        var file = _fileFixture.CreateFileWithErrors();

        // Act
        var result = await contract.TestValidateAsync(file);

        // Assert
        result.ShouldHaveAnyValidationError();
    }

    [Fact(DisplayName = "Should content file is empty lines and expected business contract invalid")]
    public async Task Should_ContentFileIsEmpty_Expected_ContractMustBeInvalid()
    {
        // Arrange
        var contract = new FinancialTransactionFileContentBusinessContract();
        var file = _fileFixture.CreateEmptyLineFile();

        // Act
        var result = await contract.TestValidateAsync(file);

        // Assert
        result.ShouldHaveAnyValidationError();
    }
}