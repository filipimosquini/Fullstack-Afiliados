using Backend.Core.Services.Contracts.ViewModels;
using Backend.UnitTest.Fixtures;
using FluentValidation.TestHelper;

namespace Backend.UnitTest.Services.Contracts.ViewModel;

[Trait("Contract Tests", "Sing-in view model Test")]
public class SignInViewModelContractTest : IClassFixture<SigninFixture>
{
    private readonly SigninFixture _signinFixture;

    public SignInViewModelContractTest(SigninFixture signinFixture)
    {
        _signinFixture = signinFixture;
    }

    [Fact(DisplayName = "Should validate contract when sign in view model contract is valid")]
    public void Should_ValidateContract_When_SignInViewModelContractIsValid()
    {
        // Arrange
        var contract = new SignInViewModelContract();
        var viewModel = _signinFixture.CreateWithoutErrors();

        // Act
        var result = contract.TestValidate(viewModel);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Should no validate contract when sign in view model contract is invalid")]
    public void Should_NoValidateContract_When_SignInViewModelContractIsInvalid()
    {
        // Arrange
        var contract = new SignInViewModelContract();
        var viewModel = _signinFixture.CreateWithErrors();

        // Act
        var result = contract.TestValidate(viewModel);

        // Assert
        result.ShouldHaveAnyValidationError();
    }
}