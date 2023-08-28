using Backend.Core.Services.Contracts.Business;
using Backend.UnitTest.Fixtures;
using FluentValidation.TestHelper;

namespace Backend.UnitTest.Services.Contracts.Business;

[Trait("Contract Tests", "Financial Transaction Test")]
public class FinancialTransactionBusinessContractTest : IClassFixture<FinancialTransactionFixture>
{
    private readonly FinancialTransactionFixture _financialTransactionFixture;

    public FinancialTransactionBusinessContractTest(FinancialTransactionFixture financialTransactionFixture)
    {
        _financialTransactionFixture = financialTransactionFixture;
    }

    [Fact(DisplayName = "Should financial transaction has no problems and expected contract valid")]
    public void Should_FinancialTransactionHasNoProblems_Expected_BusinessContractMustBeValid()
    {
        // Arrange
        var contract = new FinancialTransactionBusinessContract();
        var transaction = _financialTransactionFixture.CreateObjectWithoutErros();

        // Act
        var result = contract.TestValidate(transaction);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Should financial transaction has problems and expected contract invalid")]
    public void Should_FinancialTransactionHasProblems_Expected_BusinessContractMustBeInvalid()
    {
        // Arrange
        var contract = new FinancialTransactionBusinessContract();
        var transaction = _financialTransactionFixture.CreateObjectWithErros();

        // Act
        var result = contract.TestValidate(transaction);

        // Assert
        result.ShouldHaveAnyValidationError();
    }
}